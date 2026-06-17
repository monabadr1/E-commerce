using Infrastructure.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/category")]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]

        public async Task<IActionResult> AddCategories(CreatecategoryDto category)
        {
            await _categoryService.AddCategoryAsync(category);
            return Ok("category add successfully");
        }
        [HttpPut("{categoryId}")]

        public async Task<IActionResult> updatecategory(int categoryId,CategoryDto category)
        {
            if (categoryId != category.CategoryId)
                return BadRequest("categoryId mismatch");
            await _categoryService.UpdateCategoryAsync(category);
            return Ok("Update category successfully");
        }

        [HttpDelete("{categoryId}")]

        public async Task <IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryService.DeletCategoryAsync(categoryId);
            return NoContent();
        }
    }
}
