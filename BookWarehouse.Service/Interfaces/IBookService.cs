using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IBookService
    {
        List<BookDTO> GetAll();

        BookDTO GetBorrowedBook();

        BookDTO GetBySeri(string keyword);

        List<BookDTO> GetByFilter(BookFilter filter);

        BookUpdateDTO Add(BookUpdateDTO bookUpdateDTO);

        void Update(BookUpdateDTO bookUpdateDTO);

        void Delete(int id);
    }
}