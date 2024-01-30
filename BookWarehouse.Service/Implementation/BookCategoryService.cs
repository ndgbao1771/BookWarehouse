using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Interfaces;

namespace BookWarehouse.Service.Implementation
{
    public class BookCategoryService : IBookCategoryService
    {
        #region Contructor

        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IMapper _mapper;

        public BookCategoryService(IBookCategoryRepository bookCategoryRepository, IMapper mapper)
        {
            _bookCategoryRepository = bookCategoryRepository;
            _mapper = mapper;
        }

        #endregion Contructor

        #region Method

        public BookCategoryDTO Add(BookCategoryDTO bookCategoryDTO)
        {
            var datas = _mapper.Map<BookCategoryDTO, BookCategory>(bookCategoryDTO);
            _bookCategoryRepository.Add(datas);
            _bookCategoryRepository.Commit();
            return bookCategoryDTO;
        }

        public void Delete(int id)
        {
            var result = GetById(id);
            if (result == null)
            {
                return;
            }
            else
            {
                _bookCategoryRepository.Remove(id);
                _bookCategoryRepository.Commit();
            }
        }

        public List<BookCategoryDTO> GetAll()
        {
            var datas = _bookCategoryRepository.GetAllByViewSQL().ProjectTo<BookCategoryDTO>(_mapper.ConfigurationProvider).ToList();
            return datas;
        }

        public BookCategoryDTO GetById(int id)
        {
            return _mapper.Map<BookCategory, BookCategoryDTO>(_bookCategoryRepository.FindById(id));
        }

        public void Update(BookCategoryDTO bookCategoryDTO)
        {
            var result = GetById(bookCategoryDTO.Id);
            if (result == null)
            {
                return;
            }
            else
            {
                var datas = _mapper.Map<BookCategoryDTO, BookCategory>(bookCategoryDTO);
                _bookCategoryRepository.Updated(datas);
                _bookCategoryRepository.Commit();
            }
        }

        #endregion Method
    }
}