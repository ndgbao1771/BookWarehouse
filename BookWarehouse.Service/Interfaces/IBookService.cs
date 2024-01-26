using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IBookService
    {
        List<BookDTO> GetAll(BookFilter filter);

        BookDTO GetById(int id);

        BookDTO GetBorrowedBook();

        BookUpdateDTO Add(BookUpdateDTO bookUpdateDTO);

        void Update(BookUpdateDTO bookUpdateDTO);

        void Delete(int id);
    }
}