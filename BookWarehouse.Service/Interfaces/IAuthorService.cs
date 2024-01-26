using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IAuthorService
    {
        AuthorDTO GetById(int id);
        List<AuthorDTO> GetByFilter(AuthorFilter filter);

        AuthorDTO Add(AuthorDTO authorDTO);

        void Update(AuthorDTO authorDTO);

        void Delete(int id);
    }
}