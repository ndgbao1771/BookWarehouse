using BookWarehouse.DTO.Entities;
using BookWarehouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookWarehouse.Controllers
{
	[Route("Login")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;

		public LoginController(
			SignInManager<AppUser> signInManager,
			UserManager<AppUser> userManager,
			IConfiguration configuration)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			// Authenticate user using SignInManager
			var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

			if (result.Succeeded)
			{
				// Generate JWT token
				var user = await _userManager.FindByNameAsync(model.UserName);
				var token = GenerateJwtToken(user);
				return Ok(new { Token = token });
			}

			return Unauthorized();
		}

		private string GenerateJwtToken(AppUser user)
		{
			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.Role, "Admin")
            // Add more claims as needed	
        };

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwts:SecretKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwts:ExpireHours"]));

			var token = new JwtSecurityToken(
				_configuration["Jwts:ValidAudience"],
				_configuration["Jwts:ValidIssuer"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
