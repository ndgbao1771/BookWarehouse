using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using BookWarehouse.Service.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IRabbitMQBook _rabitMQBook;

        public BookController(IBookService bookService, ILogger<BookController> logger, IRabbitMQBook rabbitMQBook)
        {
            _bookService = bookService;
            _logger = logger;
            _rabitMQBook = rabbitMQBook;
        }

        [HttpGet]
        [Route("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            var datas = _bookService.ExportToExcell();
            if (datas == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
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
                _rabitMQBook.SendBookMessage(datas);
                return Ok();
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
                return Ok();
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
                    _rabitMQBook.SendBookMessage(datas);
                    return Created();
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
                var checkExist = _bookService.GetById(id);
                if(checkExist == null)
                {
                    return NotFound();
                }
                else
                {
                    _bookService.Delete(id);
                    return NoContent();
                }
            }
        }
    }
}