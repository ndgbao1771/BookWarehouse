using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/libratians")]
    public class LibrarianController : Controller
    {
        private readonly ILibrarianService _librarianService;
        private readonly ILogger<LibrarianController> _logger;

        public LibrarianController(ILibrarianService librarianService, ILogger<LibrarianController> logger)
        {
            _librarianService = librarianService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll([FromQuery] LibrarianFilter filter)
        {
            var datas = _librarianService.GetAll(filter);
            if (datas == null)
            {
                return BadRequest("No data to show!");
            }
            else
            {
                return new OkObjectResult(datas);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromQuery] int id)
        {
            if (id == 0)
            {
                return NotFound("Can't find librarian by id equal zero!");
            }
            else
            {
                return new OkObjectResult(_librarianService.GetById(id));
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddEntity([FromQuery] LibrarianDTO librarianDTO)
        {
            if (ModelState.IsValid)
            {
                _librarianService.Add(librarianDTO);
                return new OkObjectResult(librarianDTO);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateEntity(LibrarianDTO librarianDTO)
        {
            if (ModelState.IsValid)
            {
                _librarianService.Update(librarianDTO);
                return new OkObjectResult(librarianDTO);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound("Can't delete by id equal zero!");
            }
            else
            {
                _librarianService.Delete(id);
                return Ok("Delete success");
            }
        }
    }
}