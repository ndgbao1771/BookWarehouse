using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface ILibrarianService
    {
        List<LibrarianDTO> GetAll(LibrarianFilter filter);

        LibrarianDTO GetById(int id);

        LibrarianDTO Add(LibrarianDTO librarianDTO);

        void Update(LibrarianDTO librarianDTO);

        void Delete(int id);
    }
}