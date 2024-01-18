using BookWarehouse.DTO.Shared;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.DTO.Entities
{
    public class BookDetail : DomainEntity<int>
    {
        public BookDetail()
        {
        }

        public BookDetail(int bookId,string seri)
        {
            BookId = bookId;
            Seri = seri;
        }
        [Required(ErrorMessage = "Seri is required !")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Seri must have exactly 7 characters.")]
        public string Seri { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}