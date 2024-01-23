using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/bookCategory/[action]")]
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
        public IActionResult GetAllByViewSQL()
        {
            try
            {
                var datas = _bookCategoryService.GetAllByViewSQL();
                _logger.LogInformation("Get successs !");
                return new OkObjectResult(datas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get failed!");
                return new StatusCodeResult(500);
            }
            
        }

        [HttpGet]
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