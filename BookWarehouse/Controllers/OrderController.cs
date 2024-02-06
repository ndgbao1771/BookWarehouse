using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll([FromQuery] OrderFilter filter)
        {
            var datas = _orderService.GetAll(filter);
            if (datas == null)
            {
                return NotFound();
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
                var datas = _orderService.GetById(id);
                if (datas == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
            }
        }

        [HttpGet]
        [Route("{id}/books")]
        public IActionResult GetListBookProgressOfMember(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var datas = _orderService.GetListBookProgressOfMember(id);
                if (datas == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
            }
        }

        [HttpGet]
        [Route("statistics")]
        public IActionResult GetStatistics(DateTime dateStart, DateTime dateEnd)
        {
            var datas = _orderService.GetStatistics(dateStart, dateEnd);
            if (datas == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(datas);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreatedBookLoanReceipt(OrderAddDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                _orderService.Add(orderDTO);
                return Created();
            }
            else
            {
                return BadRequest("Book loan receipt can't be created because there are no books to borrow");
            }
        }

        [HttpPut]
        [Route("/order")]
        public IActionResult BookReturnReceipt(OrderUpdateDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var datas = GetById(orderDTO.Id);
                if (datas == null)
                {
                    return NotFound();
                }
                else
                {
                    _orderService.Update(orderDTO);
                    return Ok("Update success");
                }
            }
            return BadRequest("Update Failed");
        }

        [HttpDelete]
        [Route("/order/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
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
                    _orderService.Delete(id);
                    return NoContent();
                }
            }
        }
    }
}