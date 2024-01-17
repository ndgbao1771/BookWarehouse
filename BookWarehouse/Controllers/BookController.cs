using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/book/[action]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetByFilter(BookFilter filter)
        {
            var datas = _bookService.GetByFilter(filter);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _bookService.GetAll();
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetBySeri(string keyword)
        {
            var datas = _bookService.GetBySeri(keyword);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetBorrowedBook() 
        {
            var datas = _bookService.GetBorrowedBook();
            return new OkObjectResult(datas);
        }

        [HttpPost]
        public IActionResult AddEntity(BookUpdateDTO entity)
        {
            if (ModelState.IsValid)
            {
                var datas = _bookService.Add(entity);
                return new OkObjectResult(datas);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult UpdateEntity(BookUpdateDTO bookUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                _bookService.Update(bookUpdateDTO);
                return Ok(bookUpdateDTO);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return Ok(id);
        }
    }
}
