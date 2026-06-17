using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [ApiController]
    [Route("api/productimages")]
    public class ProductImageController:ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetImage(int productId) 
        { 
            var images=await _productImageService.GetByProductIdAsync(productId);

            return Ok(images);
        }
        [HttpPost]

        public async Task<IActionResult>AddImage(ImageDto dto)
        {
            await _productImageService.AddImageAsync(dto);
            return Ok();
        }
    }
}
