using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("api/admin/productvariant")]
    public class AdminProductVariantController:ControllerBase
    {
        private readonly IProductVariantService _productVariantService;
        public AdminProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpPost]
        public async Task <IActionResult> AddProductVariant(CreateProductVariantDto productVariant)
        {
           await _productVariantService.AddProductVariantAsync(productVariant);
            return Ok("variant add successfully");
        }
        [HttpPut("{productvariantId}")]

        public async Task<IActionResult> UpdateProductvariant(int productvariantId,ProductVariantDto productVariant) {

            if (productvariantId != productVariant.ProductVariantId)
                return BadRequest("productvariantId mismatch"); 
            await _productVariantService.UpdateProductVariantAsync(productVariant);
            return Ok("Productvariant update successfully");
        }

        [HttpDelete("{productvariantId}")]

        public async Task<IActionResult> Deleteproductvariant(int productvariantId) { 

            await _productVariantService.DeleteProductVariantAsync(productvariantId);
            return NoContent();
        }

    }
}
