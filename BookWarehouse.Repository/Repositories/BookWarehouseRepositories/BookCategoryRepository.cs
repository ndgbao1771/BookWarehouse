using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class BookCategoryRepository : Repository<BookCategory, int>, IBookCategoryRepository
    {
        private readonly AppDbContext _context;
        public BookCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<CategoryViewSQL> GetAllByViewSQL()
        {
            var datas = _context.categoryViewSQLs.AsQueryable();
            return datas;
        }
    }
}