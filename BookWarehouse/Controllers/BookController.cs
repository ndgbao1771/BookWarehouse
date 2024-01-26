using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("book")]
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
        [Route("")]
        public IActionResult GetAll([FromQuery]BookFilter filter)
        {
            var datas = _bookService.GetAll(filter);
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
        [Route("getborrowedbook")]
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
        [Route("")]
        public IActionResult AddEntity([FromQuery]BookUpdateDTO entity)
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
        [Route("")]
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
        [Route("{id}")]
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