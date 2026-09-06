using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Shared;
namespace API.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/Checkout")]
    public class CheckOutController:ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        public CheckOutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }
        [HttpPost]
        public async Task<IActionResult> CheckoutAsync([FromQuery]int userId,[FromBody] CheckoutDto dto)
        {
            try
            {
                await _checkoutService.CheckoutAsync(userId, dto);
                return Ok(new { message = "checkout is done" });

            } catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }

        }
    }
}
