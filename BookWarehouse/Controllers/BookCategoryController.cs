using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("bookcategory")]
    public class BookCategoryController : ControllerBase
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
                return Ok();
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
                if(datas == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(datas);
                }
            }
        }

        [HttpPost]
		[Authorize(Policy = "AdminPolicy")]
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
                    return Created();
                }
                return BadRequest("Create book category failed");
            }
        }

        [HttpPut]
		[Authorize(Policy = "AdminPolicy")]
		[Route("")]
        public IActionResult UpdateEntity(BookCategoryDTO bookCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                _bookCategoryService.Update(bookCategoryDTO);
                return Ok();
            }
            return BadRequest("Update book category failed");
        }

        [HttpDelete]
		[Authorize(Policy = "AdminPolicy")]
		[Route("{id}")]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest("Can't not delete book category by id equal zero!");
            }
            else
            {
                var checkExist = GetById(id);
                if(checkExist == null)
                {
                    return NotFound();
                }
                else
                {
                    _bookCategoryService.Delete(id);
                    return NoContent();
                }
            }
        }
    }
}