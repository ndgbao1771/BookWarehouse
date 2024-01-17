using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail, int>
    {
    }
}