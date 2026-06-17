using Infrastructure.IService;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;   
        }

        [HttpGet]

        public async Task <IActionResult> GetAllCategories()
        {
            var category = await _categoryService.GetAllCategoryAsync();
            return Ok(category);
        }

        [HttpGet("main")]

        public async Task<IActionResult> GetMainCategories()
        {
            var category = await _categoryService.GetMainCategoryAsync();
            return Ok(category);
        }

        [HttpGet("{parentcategoryId}/subcategories")]

        public async Task <IActionResult> GetSubCategories(int parentcategoryId)
        {
            var category = await _categoryService.GetSubCategoryAsync(parentcategoryId);
            return Ok(category);
        }

        [HttpGet("{categoryId}")]

        public async Task <IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _categoryService.GetByIdCategoryAsync(categoryId);
            return Ok(category);
        }

       

    }
}
