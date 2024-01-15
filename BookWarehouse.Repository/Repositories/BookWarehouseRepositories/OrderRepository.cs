using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Order> GetByNameLibrarian(string name)
        {
            var datas = _context.Orders.Where(x => x.librarian.Name.Contains(name));
            return datas;
        }

        public IQueryable<Order> GetByNameMember(string name)
        {
            var datas = _context.Orders.Where(x => x.member.Name.Contains(name));
            return datas;
        }

        public IQueryable<Order> GetByStatus(bool status)
        {
            var datas = _context.Orders.Where(x => x.Status.Equals(status));
            return datas;
        }
    }
}