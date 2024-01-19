using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorDTO> GetAllView();

        List<AuthorDTO> GetAll();

        AuthorDTO GetById(int id);

        List<AuthorDTO> GetByName(string name);

        List<AuthorDTO> GetByFilter(AuthorFilter filter);

        AuthorDTO Add(AuthorDTO authorDTO);

        void Update(AuthorDTO authorDTO);

        void Delete(int id);
    }
}