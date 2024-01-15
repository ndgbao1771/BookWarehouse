using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.Interfaces;

namespace BookWarehouse.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public BookUpdateDTO Add(BookUpdateDTO bookUpdateDTO)
        {
            var datas = _mapper.Map<BookUpdateDTO, Book>(bookUpdateDTO);
            _bookRepository.Add(datas);
            _bookRepository.Commit();
            return bookUpdateDTO;
        }

        public void Delete(int id)
        {
            _bookRepository.Remove(id);
        }

        public List<BookDTO> GetAll()
        {
            return _bookRepository.FindAll().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public BookDTO GetBySeri(string keyword)
        {
            var datas = _bookRepository.GetBySeri(keyword);
            var datasView = _mapper.Map<Book, BookDTO>(datas);
            return datasView;
        }

        public void Update(BookUpdateDTO bookUpdateDTO)
        {
            var datas = _mapper.Map<BookUpdateDTO, Book>(bookUpdateDTO);
            _bookRepository.Updated(datas);
            _bookRepository.Commit();
        }
    }
}