using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/book/[action]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllByViewSQL()
        {
            try
            {
                var datas = _bookService.GetAllByViewSql();
                _logger.LogInformation("Get success !");
                return new OkObjectResult(datas);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get failed !");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public IActionResult GetByFilter([FromQuery] BookFilter filter)
        {
            var datas = _bookService.GetByFilter(filter);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _bookService.GetAll();
            if (datas == null)
            {
                return NotFound();
            }
            else
            {
                return new OkObjectResult(datas);
            }
        }

        [HttpGet]
        public IActionResult GetBySeri(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return NotFound(new { message = "Please fill in accurately and completely Seri! " });
            }
            else
            {
                var datas = _bookService.GetBySeri(keyword);
                return new OkObjectResult(datas);
            }
        }

        [HttpGet]
        public IActionResult GetBorrowedBook()
        {
            var datas = _bookService.GetBorrowedBook();
            if (datas == null)
            {
                return NotFound();
            }
            else
            {
                return new OkObjectResult(datas);
            }
        }

        [HttpPost]
        public IActionResult AddEntity(BookUpdateDTO entity)
        {
            if (entity == null)
            {
                return BadRequest("Please fill in accurately and completely information book");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var datas = _bookService.Add(entity);
                    return new OkObjectResult(datas);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult UpdateEntity(BookUpdateDTO bookUpdateDTO)
        {
            if (bookUpdateDTO == null)
            {
                return BadRequest("Please fill in accurately and completely information book");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _bookService.Update(bookUpdateDTO);
                    return Ok(bookUpdateDTO);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest("Can't not delete by id equal zero!");
            }
            else
            {
                _bookService.Delete(id);
                return Ok(id);
            }
        }
    }
}