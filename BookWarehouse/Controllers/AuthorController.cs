using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetByFilter([FromQuery] AuthorFilter filter)
        {
            var datas = _authorService.GetByFilter(filter);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        [Route("{id}")]
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

        [HttpPost]
        [Route("")]
        public IActionResult AddEntity([FromQuery] AuthorDTO authorDTO)
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
        [Route("")]
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
        [Route("{id}")]
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