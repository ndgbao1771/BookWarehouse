using BookWarehouse.DTO.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookWarehouse.DTO
{
	public class DbInitializer
	{
		private readonly AppDbContext _context;
		private UserManager<AppUser> _userManager;
		private RoleManager<AppRole> _roleManager;

		public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task Seed()
		{
			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new AppRole()
				{
					Name = "Admin",
					NormalizedName = "Admin",
					Description = "Top manager"
				});
				await _roleManager.CreateAsync(new AppRole()
				{
					Name = "Staff",
					NormalizedName = "Staff",
					Description = "Staff"
				});
				await _roleManager.CreateAsync(new AppRole()
				{
					Name = "Customer",
					NormalizedName = "Customer",
					Description = "Customer"
				});
			}
			if (!_userManager.Users.Any())
			{
				var a = await _userManager.CreateAsync(new AppUser()
				{
					UserName = "admin",
					FirstName = "Nguyen Van",
					LastName = "Admin",
					Email = "admin@gmail.com",
					PhoneNumber = "0939796548",
					CreatedAt = DateTime.Now,
					CreatedBy = "Admin",
					UpdatedAt = DateTime.Now,
					UpdatedBy = "Admin",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
				}, "123456");
				var user = await _userManager.FindByNameAsync("admin");
				await _userManager.AddToRoleAsync(user, "Admin");
			}
		}
	}
}