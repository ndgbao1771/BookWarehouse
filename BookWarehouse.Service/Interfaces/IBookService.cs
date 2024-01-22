using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IBookService
    {
        List<BookDTO> GetAllByViewSql();
        List<BookDTO> GetAll();

        BookDTO GetBorrowedBook();

        BookDTO GetBySeri(string keyword);

        List<BookDTO> GetByFilter(BookFilter filter);

        BookUpdateDTO Add(BookUpdateDTO bookUpdateDTO);

        void Update(BookUpdateDTO bookUpdateDTO);

        void Delete(int id);
    }
}