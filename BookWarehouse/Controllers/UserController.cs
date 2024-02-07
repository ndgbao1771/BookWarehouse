using Azure.Core;
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
			return BadRequest("Invalid login attempt.");
			
		}

		private Token GeneratorToken(AppUser appUser)
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
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

					//roles
				}),
					Expires = DateTime.UtcNow.AddMinutes(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
				};

				var token = jwtToken.CreateToken(tokenDescription);
				var accesToken = jwtToken.WriteToken(token);
				var refreshToken = GenerateRefreshToken();

				var refreshTokenEntity = new RefreshToken
				{
					Id = Guid.NewGuid(),
					JwtId = token.Id,
					UserId = appUser.Id,
					Token = refreshToken,
					IsUsed = false,
					IsRevoked = false,
					IssuedAt = DateTime.UtcNow,
					ExpiredAt = DateTime.UtcNow.AddHours(1)
				};

				_context.Add(refreshTokenEntity);
				_context.SaveChanges();

				return new Token
				{
					AccessToken = accesToken,
					RefreshToken = refreshToken
				};
			}
			catch (Exception ex)
			{
				return new Token
				{
					
				}; ;
			}
		}

		[HttpPost]
		[Route("renewToken")]
		public IActionResult RenewToken(Token token)
		{
			var jwtToken = new JwtSecurityTokenHandler();
			var secretKeyBytes = Encoding.UTF8.GetBytes(_key.SecretKey);
			var param = new TokenValidationParameters
			{
				//auto provide token
				ValidateIssuer = false,
				ValidateAudience = false,

				//register in token
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

				ClockSkew = TimeSpan.Zero,
				ValidateLifetime = false
			};

			try
			{
				var tokenVerification = jwtToken.ValidateToken(token.AccessToken, param, out var validateToken);

				if(validateToken is JwtSecurityToken jwtSecurityToken)
				{
					var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
					if (!result)
					{
						return BadRequest("alg false!");
					}
				}

				var utcExpireDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
				var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
				if(expireDate > DateTime.UtcNow)
				{
					return BadRequest("Access token has not yet expired");
				}

				var storedToken = _context.refreshTokens.FirstOrDefault(x => x.Token == token.RefreshToken);
				if(storedToken == null)
				{
					return BadRequest("Refresh token does not exist");
				}

				if (storedToken.IsUsed)
				{
					return BadRequest("Refresh token has been used");
				}
				if (storedToken.IsRevoked)
				{
					return BadRequest("Refresh token has been revoked");
				}

				var jwtId = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
				if (storedToken.JwtId != jwtId)
				{
					return BadRequest("Token not match!");
				}

				//update token
				storedToken.IsRevoked = true;
				storedToken.IsUsed = true;
				_context.Update(storedToken);
				_context.SaveChanges();

				//create new token
				var user = _context.appUsers.SingleOrDefault(x => x.Id == storedToken.UserId);
				var newToken = GeneratorToken(user);

				return Ok("renew token success");
			}
			catch (Exception ex)
			{
				return BadRequest();
			}

		}

		private string GenerateRefreshToken()
		{
			var random = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(random);
				return Convert.ToBase64String(random);
			}
		}

		private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
		{
			var dateTimeInterval = new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

			return dateTimeInterval;
		}

	}
}