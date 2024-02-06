using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Models;
using BookWarehouse.Service.EntityDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookWarehouse.Controllers
{
	[Route("User")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly Jwt _key;

		public UserController(AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IOptions<Jwt> key)
		{
			_context = context;
			_signInManager = signInManager;
			_key = key.Value;
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Login(AppUserDTO appUserDTO)
		{
			var user = await _signInManager.PasswordSignInAsync(appUserDTO.UserName, appUserDTO.Password, false, lockoutOnFailure: false);
			if(user.Succeeded)
			{
				var appUser = _context.appUsers.Where(x => x.UserName == appUserDTO.UserName).FirstOrDefault();
				var token = GeneratorToken(appUser);
				return Ok(new { Token = token });

			}
			else
			{
				return BadRequest("Invalid login attempt.");
			}
		}

		private string GeneratorToken(AppUser appUser)
		{
			try
			{
				var jwtToken = new JwtSecurityTokenHandler();
				var secretKeyBytes = Encoding.UTF8.GetBytes(_key.SecretKey);
				var tokenDescription = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new[]
					{
					new Claim(ClaimTypes.Name, appUser.UserName),
					new Claim(ClaimTypes.Email, appUser.Email),

					//roles
					new Claim("TokenId", Guid.NewGuid().ToString())
				}),
					Expires = DateTime.UtcNow.AddMinutes(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
				};

				var token = jwtToken.CreateToken(tokenDescription);

				return jwtToken.WriteToken(token);
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
			
		}

	}
}