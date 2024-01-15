using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.Interfaces
{
    public interface ILibrarianService
    {
        List<LibrarianDTO> GetAll();

        LibrarianDTO GetById(int id);

        List<LibrarianDTO> GetByName(string name);

        LibrarianDTO Add(LibrarianDTO librarianDTO);

        void Update(LibrarianDTO librarianDTO);

        void Delete(int id);
    }
}