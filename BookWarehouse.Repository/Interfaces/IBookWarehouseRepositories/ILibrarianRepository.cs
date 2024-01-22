using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface ILibrarianRepository : IRepository<Librarian, int>
    {
        IQueryable<Librarian> GetByName(string name);

        IQueryable<Librarian> GetQueryable();

        IQueryable<LibratianViewSQL> GetAllByViewSql();
    }
}