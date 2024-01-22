using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/bookCategory/[action]")]
    public class BookCategoryController : Controller
    {
        private readonly IBookCategoryService _bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public IActionResult GetAllByViewSQL()
        {
            var datas = _bookCategoryService.GetAllByViewSQL();
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _bookCategoryService.GetAll();
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var datas = _bookCategoryService.GetById(id);
            return new OkObjectResult(datas);
        }

        [HttpPost]
        public IActionResult AddEntity(BookCategoryDTO bookCategoryDTO)
        {
            _bookCategoryService.Add(bookCategoryDTO);
            return new OkObjectResult(bookCategoryDTO);
        }

        [HttpPut]
        public IActionResult UpdateEntity(BookCategoryDTO bookCategoryDTO)
        {
            _bookCategoryService.Update(bookCategoryDTO);
            return new OkObjectResult(bookCategoryDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _bookCategoryService.Delete(id);
            return new OkObjectResult(id);
        }
    }
}