using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        IQueryable<Order> GetByNameMember(string name);
        IQueryable<Order> GetByNameLibrarian(string name);
        IQueryable<Order> GetByStatus(bool status);
    }
}