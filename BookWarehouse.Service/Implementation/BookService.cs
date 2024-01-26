using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.QueryExtension;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookWarehouse.DTO;
using System.Xml.Schema;

namespace BookWarehouse.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public BookService(IBookRepository bookRepository, IMapper mapper, AppDbContext context)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public BookUpdateDTO Add(BookUpdateDTO bookUpdateDTO)
        {
            var AuthorCheckReference = _context.Authors.Where(x => x.Id == bookUpdateDTO.AuthorId).FirstOrDefault();
            var BookCategoryCheckReference = _context.BookCategories.Where(x => x.Id == bookUpdateDTO.BookCategoryId).FirstOrDefault();
            if(AuthorCheckReference == null || bookUpdateDTO == null)
            {
                return bookUpdateDTO;
            }
            else
            {
                var datas = _mapper.Map<BookUpdateDTO, Book>(bookUpdateDTO);
                _bookRepository.Add(datas);
                _bookRepository.Commit();
                return bookUpdateDTO;
            }
            
        }

        public void Delete(int id)
        {
            _bookRepository.Remove(id);
            _bookRepository.Commit();
        }

        public List<BookDTO> GetAll(BookFilter filter)
        {
            var query = _bookRepository.GetAllByViewSql();
            query = query.Where(x => string.IsNullOrEmpty(filter.Name) || x.BookName == filter.Name)
                         .Where(x => string.IsNullOrEmpty(filter.Author) || x.AuthorName == filter.Author)
                         .Where(x => string.IsNullOrEmpty(filter.Seri) || x.BookSeri == filter.Seri)
                         .Where(x => string.IsNullOrEmpty(filter.Category) || x.Category == filter.Category);

            return query.ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public BookDTO GetBorrowedBook()
        {
            return _bookRepository.GetBorrowedBook().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).FirstOrDefault();
        }

        public BookDTO GetById(int id)
        {
            return _mapper.Map<Book, BookDTO>(_bookRepository.FindById(id));
        }

        public void Update(BookUpdateDTO bookUpdateDTO)
        {
            var AuthorCheckReference = _context.Authors.Where(x => x.Id == bookUpdateDTO.AuthorId).FirstOrDefault();
            var BookCategoryCheckReference = _context.BookCategories.Where(x => x.Id == bookUpdateDTO.BookCategoryId).FirstOrDefault();
            if (AuthorCheckReference == null || bookUpdateDTO == null)
            {
                return;
            }
            else
            {
                var datas = _mapper.Map<BookUpdateDTO, Book>(bookUpdateDTO);
                _bookRepository.Updated(datas);
                _bookRepository.Commit();
            }
        }
    }
}