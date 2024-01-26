using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("bookcategory")]
    public class BookCategoryController : Controller
    {
        private readonly IBookCategoryService _bookCategoryService;
        private readonly ILogger<BookCategoryController> _logger;

        public BookCategoryController(IBookCategoryService bookCategoryService, ILogger<BookCategoryController> logger)
        {
            _bookCategoryService = bookCategoryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var datas = _bookCategoryService.GetAll();
            if(datas == null)
            {
                return NotFound();
            }
            else
            {
                return new OkObjectResult(datas);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            if(id == 0)
            {
                return BadRequest("Can't not find book category by id equal zero!");
            }
            else
            {
                var datas = _bookCategoryService.GetById(id);
                return new OkObjectResult(datas);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddEntity(BookCategoryDTO bookCategoryDTO)
        {
            if(bookCategoryDTO == null)
            {
                return BadRequest();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _bookCategoryService.Add(bookCategoryDTO);
                    return new OkObjectResult(bookCategoryDTO);
                }
                return BadRequest("Create book category failed");
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateEntity(BookCategoryDTO bookCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                _bookCategoryService.Update(bookCategoryDTO);
                return new OkObjectResult(bookCategoryDTO);
            }
            return BadRequest("Update book category failed");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest("Can't not delete book category by id equal zero!");
            }
            else
            {
                _bookCategoryService.Delete(id);
                return new OkObjectResult(id);
            }
        }
    }
}