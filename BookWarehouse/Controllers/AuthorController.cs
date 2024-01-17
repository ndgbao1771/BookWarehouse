using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/author/[action]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _authorService.GetAll();
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var datas = _authorService.GetByName(name);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var datas = _authorService.GetById(id);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetByFilter([FromQuery] AuthorFilter filter)
        {
            var datas = _authorService.GetByFilter(filter);
            return new OkObjectResult(datas);
        }

        [HttpPost]
        public IActionResult AddEntity(AuthorDTO authorDTO) 
        {
            if (ModelState.IsValid)
            {
                var datas = _authorService.Add(authorDTO);
                return new OkObjectResult(datas);
            }
            return BadRequest(authorDTO);
        }

        [HttpPut]
        public IActionResult UpdateEntity(AuthorDTO authorDTO)
        {
            if (ModelState.IsValid)
            {
                _authorService.Update(authorDTO);
                return Ok("Update success");
            }
            return BadRequest(authorDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _authorService.Delete(id);
            return Ok("Delete success!");
        }
    }
}
