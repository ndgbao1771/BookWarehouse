using BookWarehouse.DTO.Shared;

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

        public string Seri { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}