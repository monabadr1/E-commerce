using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/order")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPut("{orderId}/status")]

        public async Task <IActionResult> UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return Ok("update statu of the order successfully");
            
        }

        [HttpPost("{orderId}/collect")]

        public async Task<IActionResult> CollectOrderAsync(int orderId) {
            await _orderService.CollectOrderAsync(orderId);
            return Ok("sucessfuly collect order");
        
        }

        [HttpGet]

        public async Task <IActionResult> AdminGetOrderAsync()
        {
            var order = await _orderService.AdminGetOrdersAsync();
            return Ok(order);
        }
        [HttpPost("{orderId}/confirm")]

        public async Task<IActionResult> ConfirmOrderAsync(int orderId)
        {
            await _orderService.ConfirmOrderAsync(orderId);
            return Ok("Order Confirmed sucessfully");

        }


    }
}
