using BookWarehouse.DTO.Enums;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/order")]
    public class OrderController : Controller
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
            return new OkObjectResult(_orderService.GetAll(filter));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var datas = _orderService.GetById(id);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        [Route("{id}/books")]
        public IActionResult GetListBookProgressOfMember(int id)
        {
            return new OkObjectResult(_orderService.GetListBookProgressOfMember(id));
        }

        [HttpGet]
        [Route("statistics")]
        public IActionResult GetStatistics(DateTime dateStart, DateTime dateEnd)
        {
            return new OkObjectResult(_orderService.GetStatistics(dateStart, dateEnd));
        }

        [HttpPost]
        [Route("/order")]
        public IActionResult CreatedBookLoanReceipt(OrderAddDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                if (orderDTO.BookId != 0)
                {
                    _orderService.Add(orderDTO);
                    return Ok("Add success");
                }
                else
                {
                    return BadRequest("Book loan receipt can't be created because there are no books to borrow");
                }
            }
            return BadRequest("Add Failed");
        }

        [HttpPut]
        [Route("/order")]
        public IActionResult BookReturnReceipt(OrderUpdateDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                _orderService.Update(orderDTO);
                return Ok("Update success");
            }
            return BadRequest("Update Failed");
        }

        [HttpDelete]
        [Route("/order/{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok("Delete success");
        }
    }
}