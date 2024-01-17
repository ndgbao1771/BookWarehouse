using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Author> GetByName(string name)
        {
            var datas = _context.Authors.Where(x => x.Name.Contains(name));
            return datas;
        }

        public IQueryable<Author> GetQueryable()
        {
            return _context.Authors.AsQueryable();
        }
    }
}