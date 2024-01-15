namespace BookWarehouse.DTO.EntityDTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Seri { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Author { get; set; }
        public string BookCategory { get; set; }
    }

    public class BookUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Seri { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int AuthorId { get; set; }
        public int BookCategoryId { get; set; }
    }
}