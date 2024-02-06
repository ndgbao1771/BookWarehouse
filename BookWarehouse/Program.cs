using AutoMapper;
using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Extensions;
using BookWarehouse.Models;
using BookWarehouse.Quartz;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.BookWarehouseRepositories;
using BookWarehouse.Service.AutoMappers;
using BookWarehouse.Service.Implementation;
using BookWarehouse.Service.Interfaces;
using BookWarehouse.Service.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Config Identity

builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

// Access IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
	// config Password
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 3;
	options.Password.RequiredUniqueChars = 1;

	// Config Lockout - lock user
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	// Config User.
	options.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = true;

	// Config login.
	options.SignIn.RequireConfirmedEmail = true;
	options.SignIn.RequireConfirmedPhoneNumber = false;
});

// Config Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
	// options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
	options.LoginPath = $"/login/";
	options.LogoutPath = $"/logout/";
	options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
	options.ValidationInterval = TimeSpan.FromSeconds(5);
});

#endregion Config Identity

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection, o => o.MigrationsAssembly("BookWarehouse.DTO")));

//By the way 1, Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddLogging(lg =>
	{
		lg.AddConsole();
	}
);

//register service Quartz
builder.Services.AddInfratructureQuartz();

builder.Services.AddControllers();

//RabbitMQ
builder.Services.AddScoped<IRabbitMQBook, RabbitMQBook>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

builder.Services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IBookCategoryService, BookCategoryService>();
builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ILibrarianService, LibrarianService>();

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookCategoryRepository, BookCategoryRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ILibrarianRepository, LibrarianRepository>();

builder.Services.AddScoped<DbInitializer>();
#region JWT
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwts"));
var secretKey = builder.Configuration["Jwts:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
#endregion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
				{
					//auto provide token
					ValidateIssuer = false,
					ValidateAudience = false,

					//register in token
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

					ClockSkew = TimeSpan.Zero
				});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<AppDbContext>();
	var initializer = services.GetRequiredService<DbInitializer>();
	initializer.Seed().Wait();
}

app.UseStaticFiles();
//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api/");
app.UsePathBase(new PathString("/api/"));
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();