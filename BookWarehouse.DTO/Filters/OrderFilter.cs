namespace BookWarehouse.DTO.Filters
{
    public class OrderFilter
    {
        public int? Id { get; set; }
        public string? LibrarianName { get; set; }
        public string? MemberName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}