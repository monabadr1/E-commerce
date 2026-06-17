using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(Roles ="User")]
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {

            _cartService = cartService;
        }

        [HttpGet]

        public async Task<IActionResult> GetCart()

        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("User not logged in");

            var userId = int.Parse(claim.Value);

            var cart = await _cartService.GetCartAsync(userId);

            return Ok(cart);
        }
        [HttpPost]
        public async Task<IActionResult> AddtoCart([FromQuery] int productVariantId, [FromQuery] int quantity)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _cartService.AddToCartAsync(userId, productVariantId, quantity);
            return Ok("add to cart successfuly");
        }



        [HttpPut("item")]

        public async Task<IActionResult> UpdateQuantity(UpdateCartItemDto cart)
        {
            await _cartService.UpdateQuantityAsync(cart);
            return Ok("Update cartItem successfully");
        }

        [HttpDelete]

        public async Task<IActionResult> ClearCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _cartService.ClearCartAsync(userId);
            return NoContent();

        }

        [HttpDelete("item/{cartItemId}")]
        public async Task<IActionResult> RemoveCart(int cartItemId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _cartService.RemoveCartItemAsync(userId, cartItemId);
            return NoContent();

        }
        [HttpPost("sync")]
        public async Task<IActionResult> SyncCart([FromBody] List<LocalCartItemDto> localItems)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();

            var userId = int.Parse(claim.Value);
            await _cartService.SyncCartAsync(userId, localItems);

            return Ok("Cart synced successfully");
        }

    }
}
