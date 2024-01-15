using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.Interfaces
{
    public interface IBookService
    {
        List<BookDTO> GetAll();

        BookDTO GetBySeri(string keyword);

        BookUpdateDTO Add(BookUpdateDTO bookUpdateDTO);

        void Update(BookUpdateDTO bookUpdateDTO);

        void Delete(int id);
    }
}