using Infrastructure.IRepository;
using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/admin/products")]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]

        public async Task<IActionResult> AddProductAsync([FromBody]CreateProductDto product)
        {
            await _productService.AddProductAsync(product);
            return Ok("product added successfully");
        }

        [HttpPut("{productId}")]

        public async Task<IActionResult> UpdateProductAsync(int productId,[FromBody]ProductDto product)
        {
            if (productId != product.ProductId)
                return BadRequest("product ID mismatch");
            await _productService.UpdateProductAsync(product);
            return Ok("product update successfully");
        }

        [HttpDelete("{productId}")]

        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }
    }
}
