using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail, int>
    {
        IQueryable<OrderDetail> GetBooksBorrowedInMonth(DateTime DateStart, DateTime DateEnd);
    }
}