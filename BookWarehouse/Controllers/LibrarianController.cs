using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/librarian/[action]")]
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
        public IActionResult GetAllByViewSql()
        {
            try
            {
                var datas = _librarianService.GetAllByViewSql();
                _logger.LogInformation("Get Success");
                return View(datas);
            }catch (Exception ex)
            {
                _logger.LogError("Get failed !");
                return new StatusCodeResult(500);
            }
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _librarianService.GetAll();
            if(datas == null)
            {
                return BadRequest("No data to show!");
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
                return NotFound("Can't find librarian by id equal zero!");
            }
            else
            {
                return new OkObjectResult(_librarianService.GetById(id));
            }
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Please fill in accurately and completely librarian name!");
            }
            else
            {
                return new OkObjectResult(_librarianService.GetByName(name));
            }
        }

        [HttpGet]
        public IActionResult GetByFilter([FromQuery] LibrarianFilter filter)
        {
            return new OkObjectResult(_librarianService.GetByFilter(filter));
        }

        [HttpPost]
        public IActionResult AddEntity(LibrarianDTO librarianDTO)
        {
            if (ModelState.IsValid)
            {
                _librarianService.Add(librarianDTO);
                return new OkObjectResult(librarianDTO);
            }
            return BadRequest();
        }

        [HttpPut]
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
        public IActionResult Delete(int id)
        {
            if(id == 0)
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