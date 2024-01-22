using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class LibrarianRepository : Repository<Librarian, int>, ILibrarianRepository
    {
        private readonly AppDbContext _context;

        public LibrarianRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<LibratianViewSQL> GetAllByViewSql()
        {
            return _context.libratianViewSQLs;
        }

        public IQueryable<Librarian> GetByName(string name)
        {
            var datas = _context.Librarians.Where(x => x.Name == name);
            return datas;
        }

        public IQueryable<Librarian> GetQueryable()
        {
            return _context.Librarians.AsQueryable();
             
        }
    }
}