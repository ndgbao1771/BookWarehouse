using BookWarehouse.DTO.Enums;
using BookWarehouse.DTO.Shared;

namespace BookWarehouse.DTO.Entities
{
    public class Order : DomainEntity<int>
    {
        public Order(StatusAble status)
        {
            Status = status;
        }

        public Order(int memberId, int librarianId, DateTime dateCreated, StatusAble status)
        {
            MemberId = memberId;
            LibrarianId = librarianId;
            DateCreated = dateCreated;
            Status = status;
        }

        public StatusAble Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int MemberId { get; set; }
        public Member member { get; set; }
        public int LibrarianId { get; set; }
        public Librarian librarian { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}