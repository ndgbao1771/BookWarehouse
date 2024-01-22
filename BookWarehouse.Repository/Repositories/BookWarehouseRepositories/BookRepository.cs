using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
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


        public IQueryable<Book> GetBorrowedBook()
        {
            var datas = _context.Orders.SelectMany(x => x.orderDetails.Select(x => x.book))
                                       .GroupBy(book => book.Id)
                                       .OrderByDescending(x => x.Count())
                                       .Select(x => x.Key);
            var result = _context.Books.Where(x => datas.Contains(x.Id));
            return result;
        }

        public IQueryable<Book> GetQueryable()
        {
            return _context.Books.AsQueryable();
        }

        public IQueryable<BookViewSQL> GetAllByViewSql()
        {
            return _context.bookViewSQLs;
        }
    }
}