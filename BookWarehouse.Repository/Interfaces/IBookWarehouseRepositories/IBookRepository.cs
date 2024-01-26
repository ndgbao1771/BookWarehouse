using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IBookRepository : IRepository<Book, int>
    {
        IQueryable<Book> GetBorrowedBook();
        IQueryable<BookViewSQL> GetAllByViewSql();
    }
}