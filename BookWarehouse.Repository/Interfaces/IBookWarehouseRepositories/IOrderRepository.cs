using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        IQueryable<Order> GetById(int id);

        IQueryable<Order> GetListBookProgressOfMember(int id);

        IQueryable<Order> GetStatistics(DateTime dateStart, DateTime dateEnd);

        IQueryable<Order> GetBooksBorrowedInMonth(DateTime DateStart, DateTime DateEnd);

        IQueryable<OrderViewSQL> GetAllByViewSql();
    }
}