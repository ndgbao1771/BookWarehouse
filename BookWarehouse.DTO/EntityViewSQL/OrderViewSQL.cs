using System.ComponentModel.DataAnnotations.Schema;

namespace BookWarehouse.DTO.EntityViewSQL
{
    public class OrderViewSQL
    {
        [Column("LibratianName")]
        public string LibratianName { get;  }

        [Column("MemberName")]
        public string MemberName { get;  }

        [Column("BookName")]
        public string BookName { get;  }

        [Column("DateCreated")]
        public DateTime DateCreated { get;  }

        [Column("ExpectedReturnDate")]
        public DateTime ExpectedReturnDate { get;  }

        [Column("ActualReturnDate")]
        public DateTime ActualReturnDate { get;  }

        [Column("OrderStatus")]
        public int OrderStatus { get;  }
    }
}