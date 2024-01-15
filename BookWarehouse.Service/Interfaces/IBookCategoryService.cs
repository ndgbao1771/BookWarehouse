using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.Interfaces
{
    public interface IBookCategoryService
    {
        List<BookCategoryDTO> GetAll();

        BookCategoryDTO GetById(int id);

        BookCategoryDTO Add(BookCategoryDTO bookCategoryDTO);

        void Update(BookCategoryDTO bookCategoryDTO);

        void Delete(int id);
    }
}