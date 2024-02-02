using AutoMapper;
using BookWarehouse.Auth;
using BookWarehouse.DTO;
using BookWarehouse.Extensions;
using BookWarehouse.Quartz;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.BookWarehouseRepositories;
using BookWarehouse.Service.AutoMappers;
using BookWarehouse.Service.Implementation;
using BookWarehouse.Service.Interfaces;
using BookWarehouse.Service.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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
//By the way 2, Add support to logging with SERILOG
/*builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Information)
                .CreateLogger();*/

//register service Quartz
builder.Services.AddInfratructureQuartz();

builder.Services.AddControllers();

//RabbitMQ
builder.Services.AddScoped<IRabbitMQBook, RabbitMQBook>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api/");
app.UseMiddleware<BasicAuthHandler>("Basic");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UsePathBase(new PathString("/api/"));
app.UseRouting();

app.Run();