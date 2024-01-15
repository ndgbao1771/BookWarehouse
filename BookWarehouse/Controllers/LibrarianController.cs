using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/librarian/[action]")]
    public class LibrarianController : Controller
    {
        private readonly ILibrarianService _librarianService;

        public LibrarianController(ILibrarianService librarianService)
        {
            _librarianService = librarianService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_librarianService.GetAll());
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            return new OkObjectResult(_librarianService.GetById(id));
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            return new OkObjectResult(_librarianService.GetByName(name));
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
            _librarianService.Delete(id);
            return Ok("Delete success");
        }
    }
}
