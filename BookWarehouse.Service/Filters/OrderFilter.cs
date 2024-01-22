using BookWarehouse.DTO.Enums;

namespace BookWarehouse.Service.Filters
{
    public class OrderFilter
    {
        public int? Id { get; set; }
        public string? LibrarianName { get; set; }
        public string? MemberName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public StatusAble? StatusAble { get; set; }
        public DateTime? DateGiveCurrent { get; set; }
    }
}