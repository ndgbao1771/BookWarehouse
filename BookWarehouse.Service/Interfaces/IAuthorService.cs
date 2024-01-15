using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorDTO> GetAll();

        AuthorDTO GetById(int id);

        List<AuthorDTO> GetByName(string name);

        AuthorDTO Add(AuthorDTO authorDTO);

        void Update(AuthorDTO authorDTO);

        void Delete(int id);
    }
}