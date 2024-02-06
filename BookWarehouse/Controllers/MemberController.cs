using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/member")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<MemberController> _logger;

        public MemberController(IMemberService memberService, ILogger<MemberController> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll([FromQuery] MemberFilter filter)
        {
            var datas = _memberService.GetAll(filter);
            if (datas == null)
            {
                return NotFound("No data to show");
            }
            else
            {
                return Ok(datas);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                var datas = _memberService.GetById(id);
                if (datas == null)
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
        [Route("")]
        public IActionResult AddEntity([FromQuery] MemberDTO memberDTO)
        {
            if (ModelState.IsValid)
            {
                _memberService.Add(memberDTO);
                return Created();
            }
            return BadRequest("Add Failed");
        }

        [HttpPut]
        [Route("")]
        public IActionResult UpdateEntity(MemberDTO memberDTO)
        {
            if (ModelState.IsValid)
            {
                _memberService.Update(memberDTO);
                return Ok(memberDTO);
            }
            return BadRequest("Update Failed");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Can't delete member by id equal zero!");
            }
            else
            {
                var checkExist = GetById(id);
                if (checkExist == null)
                {
                    return NotFound();
                }
                else
                {
                    _memberService.Delete(id);
                    return NoContent();
                }
            }
        }
    }
}