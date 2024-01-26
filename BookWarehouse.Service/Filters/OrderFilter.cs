using BookWarehouse.DTO.Enums;

namespace BookWarehouse.Service.Filters
{
    public class OrderFilter
    {
        public string? LibrarianName { get; set; }
        public string? MemberName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? StatusAble { get; set; }
        public DateTime? DateGiveCurrent { get; set; }
    }
}