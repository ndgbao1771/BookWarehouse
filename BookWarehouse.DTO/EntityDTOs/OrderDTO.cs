using BookWarehouse.DTO.Enums;

namespace BookWarehouse.DTO.EntityDTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public string LibrarianName { get; set; }
        public DateTime DateGiveCurent { get; set; }
        public DateTime DateGiveExpect { get; set; }
        public List<OrderDetailDTO> orderDetails { get; set; }
    }

    public class OrderAddDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int LibrarianId { get; set; }
        public int BookId {  get; set; }
        public StatusAble Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateGiveCurent { get; set; }
        public DateTime DateGiveExpect { get; set; }
    }

    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public StatusAble Status { get; set; }
        public DateTime DateGiveCurent { get; set; }
    }
}