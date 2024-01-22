using BookWarehouse.DTO;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/author/[action]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly AppDbContext _context;

        public AuthorController(IAuthorService authorService, AppDbContext context)
        {
            _authorService = authorService;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllByViewSQL()
        {
            var datas = _authorService.GetAllByViewSQL();
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _authorService.GetAll();
            if (datas == null)
            {
                return NotFound(); //Error 404
            }
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var datas = _authorService.GetByName(name);
            if (datas == null)
            {
                return NotFound(); //Error 404
            }
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return NotFound(new { message = "Can't find Author by id equal zero!" });
            }
            else
            {
                var datas = _authorService.GetById(id);
                if (datas == null)
                {
                    return NotFound(); //Error 404
                }
                else
                {
                    return new OkObjectResult(datas);
                }
            }
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
            else
            {
                return BadRequest("Model is null");
            }
        }

        [HttpPut]
        public IActionResult UpdateEntity(AuthorDTO authorDTO)
        {
            if (ModelState.IsValid)
            {
                _authorService.Update(authorDTO);
                return Ok(authorDTO);
            }
            return BadRequest(authorDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Can't delete by id equal zero !");
            }
            else
            {
                _authorService.Delete(id);
                return Ok("Delete success!");
            }
        }
    }
}