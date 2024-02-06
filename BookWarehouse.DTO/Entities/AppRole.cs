using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.DTO.Entities
{
	public class AppRole : IdentityRole<int>
	{
		[StringLength(250)]
		public string Description { get; set; }
	}
}