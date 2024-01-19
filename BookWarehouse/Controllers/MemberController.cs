using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/member/[action]")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult GetByFilter([FromQuery] MemberFilter filter)
        {
            return new OkObjectResult(_memberService.GetByFilter(filter));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var datas = _memberService.GetAll();
            if (datas == null)
            {
                return NotFound("No data to show");
            }
            else
            {
                return new OkObjectResult(datas);
            }
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                return new OkObjectResult(_memberService.GetById(id));
            }
        }

        [HttpGet]
        public IActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }
            else
            {
                return new OkObjectResult(_memberService.GetByName(name));
            }
        }

        [HttpPost]
        public IActionResult AddEntity(MemberDTO memberDTO)
        {
            if (ModelState.IsValid)
            {
                _memberService.Add(memberDTO);
                return new OkObjectResult(memberDTO);
            }
            return BadRequest("Add Failed");
        }

        [HttpPut]
        public IActionResult UpdateEntity(MemberDTO memberDTO)
        {
            if (ModelState.IsValid)
            {
                _memberService.Update(memberDTO);
                return new OkObjectResult(memberDTO);
            }
            return BadRequest("Update Failed");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Can't delete member by id equal zero!");
            }
            else
            {
                _memberService.Delete(id);
                return new OkObjectResult("Delete success");
            }
        }
    }
}