using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/productvariant")]

    public class ProductvariantController : ControllerBase
    {
        private readonly IProductVariantService _productVariantService;

        public ProductvariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }
        [HttpGet("{productvariantId}")]
        public async Task<IActionResult> GetById(int productvariantId)
        {
            var variant = await _productVariantService.GetByIdAsync(productvariantId);
            if (variant==null)
                return NotFound("product variant not found");
            return Ok(variant);
        }
        [HttpGet("product/{productId}")]

        public async Task<IActionResult> GetByProductId(int productId) { 
            var product =await _productVariantService.GetByProductIdAsync(productId);
            return Ok(product);
        
        }
    }
}
