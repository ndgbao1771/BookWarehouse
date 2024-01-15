using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class BookCategoryRepository : Repository<BookCategory, int>, IBookCategoryRepository
    {
        public BookCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}