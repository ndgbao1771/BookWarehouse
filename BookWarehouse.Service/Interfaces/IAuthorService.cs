using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorDTO> GetAllByViewSQL();
        List<AuthorDTO> GetAll();

        AuthorDTO GetById(int id);

        List<AuthorDTO> GetByName(string name);

        List<AuthorDTO> GetByFilter(AuthorFilter filter);

        AuthorDTO Add(AuthorDTO authorDTO);

        void Update(AuthorDTO authorDTO);

        void Delete(int id);
    }
}