using BookWarehouse.DTO.Shared;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Expected return date is required !")]
        [Display(Name = "Expected Return Date")]
        [DataType(DataType.DateTime, ErrorMessage = "DateTime format incorrect !")]
        public DateTime DateGiveExpect { get; set; }

        [Required(ErrorMessage = "Actual return date is required !")]
        [Display(Name = "Actual Return Date")]
        [DataType(DataType.DateTime, ErrorMessage = "DateTime format incorrect !")]
        public DateTime DateGiveCurrent { get; set; }

        public int OrderId { get; set; }
        public Order order { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}