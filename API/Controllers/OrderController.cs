using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) { 

            _orderService = orderService;
        }

        [HttpGet("{orderId}")]

        public async Task <IActionResult> GetOrderAsync(int orderId)
        {
           var order =await _orderService.GetOrderAsync(orderId);
            if (order == null)
               return NotFound("order not found");

            return Ok(order);
        }

        [HttpGet("user/{userId}")]

        public async Task <IActionResult> GetOrdersAsync(int userId)
        {
            var orders =await _orderService.GetOrdersAsync(userId);
            return Ok(orders);
        }


    }
}
