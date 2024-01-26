using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<AuthorViewSQL> GetQueryable()
        {
            return _context.authorViewSQLs.AsQueryable();
        }
    }
}