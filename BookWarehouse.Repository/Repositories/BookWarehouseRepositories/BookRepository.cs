using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class BookRepository : Repository<Book, int>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Book GetBySeri(string keyword)
        {
            BookDetail bookDetail = _context.BookDetails.FirstOrDefault(x => x.Seri == keyword);
            Book datas = _context.Books.FirstOrDefault(x => x.Id == bookDetail.BookId);
            return datas;
        }
    }
}