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

        public List<BookDTO> GetAll()
        {
            return _bookRepository.FindAll().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public BookDTO GetBorrowedBook()
        {
            return _bookRepository.GetBorrowedBook().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).FirstOrDefault();
        }

        public BookDTO GetBySeri(string keyword)
        {
            var datas = _bookRepository.GetBySeri(keyword);
            var datasView = _mapper.Map<Book, BookDTO>(datas);
            return datasView;
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

        public List<BookDTO> GetByFilter(BookFilter filter)
        {
            var query = _bookRepository.GetQueryable();
            query = query.Include(bd => bd.bookDetails)
                         .Where(x => string.IsNullOrEmpty(filter.Seri) || x.bookDetails.Select(x => x.Seri).ToList().Contains(filter.Seri))
                         .Where(x => filter.Id == null || x.Id == filter.Id)
                         .Where(BookQueryExtension.BookNameFilter(filter))
                         .Where(BookQueryExtension.AuthorNameFilter(filter));
            return query.ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public List<BookDTO> GetAllByViewSql()
        {
            var datas = _bookRepository.GetAllByViewSql().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();
            return datas;
        }
    }
}