using BookWarehouse.DTO.EntityDTOs;
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
        public IActionResult GetAll()
        {
            return new OkObjectResult(_orderService.GetAll());
        }

        [HttpGet]
        public IActionResult GetById(int id) 
        {
            return new OkObjectResult(_orderService.GetById(id));
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
        public IActionResult GetByStatus(bool status)
        {
            return new OkObjectResult(_orderService.GetByStatus(status));
        }

        [HttpPost]
        public IActionResult AddEntity(OrderAddDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                _orderService.Add(orderDTO);
                return Ok("Add success");
            }
            return BadRequest("Add Failed");
        }

        [HttpPut]
        public IActionResult UpdateEntity(OrderUpdateDTO orderDTO)
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
