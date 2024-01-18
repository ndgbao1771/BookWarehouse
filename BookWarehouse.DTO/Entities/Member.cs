using BookWarehouse.DTO.Shared;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.DTO.Entities
{
    public class Member : DomainEntity<int>, IDateTracking
    {
        public Member(string name, DateTime createdAt, string createdBy, DateTime updatedAt, string updatedBy)
        {
            Name = name;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }

        [Required(ErrorMessage = "Member name is required !")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Member name must be at least 6 characters and no more than 100 characters.")]
        public string Name { get; set; }

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