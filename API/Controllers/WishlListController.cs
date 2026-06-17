using Infrastructure.IService;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/wishlist")]
    public class WishlListController : ControllerBase
    {
        private readonly IwishlistService _wishlistService;
        public WishlListController(IwishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        [HttpPost("items")]

        public async Task<IActionResult> AddWishListAsync( int productVariantId)

        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("User not logged in");

            var userId = int.Parse(claim.Value);
            await _wishlistService.AddWishListAsync(userId,productVariantId);
            return Ok("WishList is add successfully");
        }
        [HttpDelete("items/{wishlistitemId}")]

        public async Task<IActionResult> DeletItemWishList(int wishlistitemId)
        {
            await _wishlistService.DeletItemWishListAsync(wishlistitemId);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetWishListByUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("User not logged in");

            var userId = int.Parse(claim.Value);
            var wishlist =await _wishlistService.GetWishListByUserIdAsync(userId);
            return Ok(wishlist);
        }
        [HttpGet("items/{wishlistiemId}")]
       public async Task<IActionResult> GetWishListItemAsync(int wishlistiemId)
        {
            var wishlistItem=await _wishlistService.GetWishListItemAsync(wishlistiemId);
            return Ok(wishlistItem);    
        }
        [HttpPost("sync")]
        public async Task<IActionResult> SyncWishList([FromBody] List<FavoriteItemDto> favoriteItems)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();

            var userId = int.Parse(claim.Value);
            await _wishlistService.SyncWishListAsync(userId, favoriteItems);

            return Ok("WishList synced successfully");
        }

        [HttpDelete]

        public async Task<IActionResult> ClearWishlist()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _wishlistService.ClearWishlistAsync(userId);
            return NoContent();

        }
    }
}
