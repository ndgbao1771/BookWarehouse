using BookWarehouse.DTO.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWarehouse.DTO.Entities
{
	[Table("RefreshToken")]
	public class RefreshToken
	{
		public Guid Id { get; set; }
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public AppUser appUser { get; set; }
		public string Token { get; set; }
		public string JwtId { get; set; }
		public bool IsUsed { get; set; }
		public bool IsRevoked { get; set; }
		public DateTime IssuedAt { get; set; }
		public DateTime ExpiredAt { get; set;}
	}
}