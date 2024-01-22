using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.DTO.EntityViewSQL
{
    public class OrderViewSQL
    {
        public string LibratianName { get;  }
        public string MemberName { get;  }
        public string BookName { get;  }
        public DateTime DateCreated { get;  }
        public DateTime ExpectedReturnDate { get;  }
        public DateTime ActualReturnDate { get;  }
        public int OrderStatus { get;  }
    }
}