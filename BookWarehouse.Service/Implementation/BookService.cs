using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

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
            if (AuthorCheckReference == null || bookUpdateDTO == null)
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
            var result = GetById(id);
            if (result == null)
            {
                return;
            }
            else
            {
                _bookRepository.Remove(id);
                _bookRepository.Commit();
            }
        }

        public FileInfo ExportToExcell()
        {
            var datas = _bookRepository.FindAll().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();

            string directoryPath = Path.Combine("D:\\Developer\\SiliconStack\\Project2\\BookWarehouse\\BookWarehouse", "Excel");
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath,"List_Book.xlsx");

            FileInfo newFile = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                if (newFile.Exists)
                {
                    newFile.Delete();
                    newFile = new FileInfo(filePath);
                }
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Inventory");

                if (worksheet != null)
                {
                    package.Workbook.Worksheets.Delete("Inventory");
                }

                worksheet = package.Workbook.Worksheets.Add("Inventory");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Seri";
                worksheet.Cells[1, 4].Value = "CreatedAt";
                worksheet.Cells[1, 5].Value = "CreatedBy";
                worksheet.Cells[1, 6].Value = "UpdatedAt";
                worksheet.Cells[1, 7].Value = "UpdatedBy";
                worksheet.Cells[1, 8].Value = "Author";
                worksheet.Cells[1, 9].Value = "BookCategory";

                int row = 2;
                foreach (var data in datas)
                {
                    worksheet.Cells[row, 1].Value = data.Id;
                    worksheet.Cells[row, 2].Value = data.Name;
                    worksheet.Cells[row, 3].Value = data.Seri;
                    worksheet.Cells[row, 4].Value = data.CreatedAt;
                    worksheet.Cells[row, 5].Value = data.CreatedBy;
                    worksheet.Cells[row, 6].Value = data.UpdatedAt;
                    worksheet.Cells[row, 7].Value = data.UpdatedBy;
                    worksheet.Cells[row, 8].Value = data.Author;
                    worksheet.Cells[row, 9].Value = data.BookCategory;

                    row++;
                }

                worksheet.Cells[2, 4, row, 7].Style.Numberformat.Format = "yyyy-mm-dd";
                worksheet.Cells[2, 4, row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                worksheet.Cells[$"I2:I{row}"].Formula = "C2*D2";

                package.Save();
                package.Dispose();
            }

            return newFile;
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
                var result = GetById(bookUpdateDTO.Id);
                if (result == null)
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
}