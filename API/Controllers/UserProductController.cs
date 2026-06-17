using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class UserProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public UserProductController(IProductService productService) { 
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }
        [HttpGet("{productId}")]

        public async Task <IActionResult> GetProductByIdAsync(int productId)
        {
            var product=await _productService.GetProductByIdAsync(productId);
            if (product == null)
                return NotFound("product not found");
            return Ok(product);
        }

        [HttpGet("maincategory/{mainCategoryId}")]

        public async Task<IActionResult> GetByMainCategoryAsync(int maincategoryId) {
            var products = await _productService.GetProductsByMainCategoryAsync(maincategoryId);
            return Ok(products);
        }

        [HttpGet("category/{categoryId}")]

        public async Task <IActionResult> GetproductBycategoryAsync(int categoryId)
        {
            var category =await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(category);
        }

    }
}
