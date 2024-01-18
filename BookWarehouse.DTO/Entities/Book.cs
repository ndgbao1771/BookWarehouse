using BookWarehouse.DTO.Shared;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.DTO.Entities
{
    public class Book : DomainEntity<int>, IDateTracking
    {
        public Book()
        {
        }

        public Book(string name, DateTime createdAt, string createdBy, DateTime updatedAt, string updatedBy)
        {
            Name = name;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }

        public Book(string name, DateTime createdAt, string createdBy, DateTime updatedAt, string updatedBy, int authorId, int bookCategoryId)
        {
            Name = name;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            AuthorId = authorId;
            BookCategoryId = bookCategoryId;
        }

        [Required(ErrorMessage = "Book name is required !")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Book name must be at least 6 characters and no more than 100 characters.")]
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

        public int AuthorId { get; set; }
        public Author author { get; set; }

        public int BookCategoryId { get; set; }
        public BookCategory bookCategory { get; set; }

        public List<BookDetail> bookDetails { get; set; }
    }
}