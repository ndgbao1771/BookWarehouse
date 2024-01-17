using BookWarehouse.DTO.Enums;

namespace BookWarehouse.DTO.EntityDTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime DateGiveExpect { get; set; }
        public DateTime DateGiveCurrent { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
    }

    public class StatisticsDTO
    {
        public string BorrowerName { get; set; }
        public string LibrarianName { get; set; }
        public string BookName { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public StatusAble Status { get; set; }
    }
}