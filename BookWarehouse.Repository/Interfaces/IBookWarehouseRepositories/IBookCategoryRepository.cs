using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IBookCategoryRepository : IRepository<BookCategory, int>
    {
        IQueryable<CategoryViewSQL> GetAllByViewSQL();
    }
}