using BookWarehouse.DTO.Shared;

namespace BookWarehouse.DTO.Entities
{
    public class OrderDetail : DomainEntity<int>
    {
        public OrderDetail() { }

        public OrderDetail(int orderId,int bookId, DateTime dateCreated, DateTime dateGiveCurrent, DateTime dateGiveExpect) 
        {
            this.OrderId = orderId;
            BookId = bookId;
            DateGiveCurrent = dateGiveCurrent;
            DateGiveExpect = dateGiveExpect;
        }

        public OrderDetail(DateTime dateGiveCurrent) 
        {
            DateGiveCurrent = dateGiveCurrent;
        }
        public DateTime DateGiveExpect { get; set; }
        public DateTime DateGiveCurrent { get; set; }

        public int OrderId { get; set; }
        public Order order { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}