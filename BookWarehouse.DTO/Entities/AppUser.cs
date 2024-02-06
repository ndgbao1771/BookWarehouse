using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.DTO.Entities
{
	public class AppUser : IdentityUser<int>
	{
		[Required(ErrorMessage = "Member first name is required !")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Member first name must be at least 1 characters and no more than 100 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Member last name is required !")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Member last name must be at least 1 characters and no more than 100 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Date created is required !")]
		[Display(Name = "Date created")]
		[DataType(DataType.DateTime, ErrorMessage = "DateTime format incorrect !")]
		public DateTime CreatedAt { get; set; }

		[Required(ErrorMessage = "Creator is required !")]
		[StringLength(50, MinimumLength = 4, ErrorMessage = "Creator must be at least 4 charactors and no more than 50 characters")]
		public string CreatedBy { get; set; }

		[Required(ErrorMessage = "Date update is required !")]
		[Display(Name = "Date update")]
		[DataType(DataType.DateTime, ErrorMessage = "DateTime format incorrect !")]
		public DateTime UpdatedAt { get; set; }

		[Required(ErrorMessage = "Updater is required !")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Updater must be at 4 and characters and no more than 50 characters ")]
		public string UpdatedBy { get; set; }

		public List<Order> orders { get; set; }
	}
}