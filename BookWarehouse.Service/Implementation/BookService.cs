using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using OfficeOpenXml;

namespace BookWarehouse.Service.Implementation
{
    public class BookService : IBookService
    {
        #region Contructor

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public BookService(IBookRepository bookRepository, IMapper mapper, AppDbContext context)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        #endregion Contructor

        #region Method

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

        #endregion Method

        #region Extension Method Export file excel

        #region First way

        /*public FileInfo ExportToExcell()
        {
            var datas = _bookRepository.GetAllByViewSql().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();
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
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Danh sách sách trong kho");

                if (worksheet != null)
                {
                    package.Workbook.Worksheets.Delete("Danh sách sách trong kho");
                }

                worksheet = package.Workbook.Worksheets.Add("Danh sách sách trong kho");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Tên";
                worksheet.Cells[1, 3].Value = "Seri";
                worksheet.Cells[1, 4].Value = "Tác giả";
                worksheet.Cells[1, 5].Value = "Danh mục";
                worksheet.Cells[1, 6].Value = "Ngày tạo";
                worksheet.Cells[1, 7].Value = "Người tạo";
                worksheet.Cells[1, 8].Value = "Ngày cập nhật";
                worksheet.Cells[1, 9].Value = "Người cập nhật";

                int row = 2;
                foreach (var data in datas)
                {
                    worksheet.Cells[row, 1].Value = data.Id;
                    worksheet.Cells[row, 2].Value = data.Name;
                    worksheet.Cells[row, 3].Value = data.Seri;
                    worksheet.Cells[row, 4].Value = data.Author;
                    worksheet.Cells[row, 5].Value = data.BookCategory;
                    worksheet.Cells[row, 6].Value = data.CreatedAt;
                    worksheet.Cells[row, 7].Value = data.CreatedBy;
                    worksheet.Cells[row, 8].Value = data.UpdatedAt;
                    worksheet.Cells[row, 9].Value = data.UpdatedBy;

                    row++;
                }

                worksheet.Cells[2, 5, row, 8].Style.Numberformat.Format = "dd-mm-yyyy";

                package.Save();
                package.Dispose();
            }
            return newFile;
        }*/

        #endregion First way

        #region Second way

        public FileInfo ExportToExcell()
        {
            var datas = _bookRepository.GetAllByViewSql().ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToList();

            string templateFilePath = "D:\\Developer\\SiliconStack\\Project2\\BookWarehouse\\BookWarehouse\\Excel\\Template\\List_Book.xlsx";

            string directoryPath = Path.Combine("D:\\Developer\\SiliconStack\\Project2\\BookWarehouse\\BookWarehouse", "Excel");
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "List_Book.xlsx");

            FileInfo newFile = new FileInfo(filePath);

            File.Copy(templateFilePath, filePath, true);

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int row = 2;
                foreach (var data in datas)
                {
                    worksheet.Cells[row, 1].Value = data.Id;
                    worksheet.Cells[row, 2].Value = data.Name;
                    worksheet.Cells[row, 3].Value = data.Seri;
                    worksheet.Cells[row, 4].Value = data.Author;
                    worksheet.Cells[row, 5].Value = data.BookCategory;
                    worksheet.Cells[row, 6].Value = data.CreatedAt;
                    worksheet.Cells[row, 7].Value = data.CreatedBy;
                    worksheet.Cells[row, 8].Value = data.UpdatedAt;
                    worksheet.Cells[row, 9].Value = data.UpdatedBy;

                    row++;
                }

                worksheet.Cells[2, 5, row, 8].Style.Numberformat.Format = "dd-mm-yyyy";

                package.Save();
                package.Dispose();
            }

            return newFile;
        }

        #endregion Second way

        #endregion Extension Method Export file excel
    }
}