using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Enums;
using BookWarehouse.DTO.Filters;
using BookWarehouse.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookWarehouse.Controllers
{
    [ApiController]
    [Route("/api/order/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetByFilter([FromQuery]OrderFilter filter)
        {
            return new OkObjectResult(_orderService.GetByFilter(filter));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_orderService.GetAll());
        }

        [HttpGet]
        public IActionResult GetById(int id) 
        {
            var datas = _orderService.GetById(id);
            return new OkObjectResult(datas);
        }

        [HttpGet]
        public IActionResult GetByNameMember(string name)
        {
            return new OkObjectResult(_orderService.GetByNameMember(name));
        }

        [HttpGet]
        public IActionResult GetByNameLib(string name)
        {
            return new OkObjectResult(_orderService.GetByNameLibrarian(name));
        }

        [HttpGet]
        public IActionResult GetByStatus(StatusAble status)
        {
            return new OkObjectResult(_orderService.GetByStatus(status));
        }

        [HttpGet]
        public IActionResult GetListBookProgressOfMember(int id)
        {
            return new OkObjectResult(_orderService.GetListBookProgressOfMember(id));
        }

        [HttpGet]
        public IActionResult GetStatistics(DateTime dateStart, DateTime dateEnd)
        {
            return new OkObjectResult(_orderService.GetStatistics(dateStart, dateEnd));
        }

        [HttpPost]
        public IActionResult CreatedBookLoanReceipt(OrderAddDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                if(orderDTO.BookId != 0)
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
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok("Delete success");
        }
    }
}
