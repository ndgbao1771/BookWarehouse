using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class OrderDetailRepository : Repository<OrderDetail, int>, IOrderDetailRepository
    {
        private readonly AppDbContext _context;

        public OrderDetailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<OrderDetail> GetBooksBorrowedInMonth(DateTime DateStart, DateTime DateEnd)
        {
            var datas = _context.OrderDetails.Where(x => x.DateCreated >= DateStart && x.DateCreated <= DateEnd);
            return datas;
        }
    }
}