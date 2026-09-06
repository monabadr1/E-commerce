using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class CategoryConsumer
    {
        private readonly HttpClient _http;

        public CategoryConsumer(HttpClient http)
        {
            _http = http;
        }

       public async Task<List<CategoryDto>?> GetAllCategoryAsync()
        {
            var category = await _http.GetAsync("api/category");
            var content=await category.Content.ReadAsStringAsync();
            if (!category.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await category.Content.ReadFromJsonAsync<List<CategoryDto>>();
        }
        public async Task<List<CategoryDto>?> GetMainCategoryAsync()
        {
            var category = await _http.GetAsync("api/category/main");
            var content = await category.Content.ReadAsStringAsync();
            if (!category.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            return await category.Content.ReadFromJsonAsync<List<CategoryDto>>();
        }

       public async Task<List<CategoryDto>?> GetSubCategoryAsync(int parentCategory)
        {
            var categoey = await _http.GetAsync($"api/category/{parentCategory}/subcategories");
            var content=await categoey.Content.ReadAsStringAsync();
            if (!categoey.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await categoey.Content.ReadFromJsonAsync<List<CategoryDto>?>();
        }

        public async Task<CategoryDto?> GetByIdCategoryAsync(int categoryId)
        {
            var category = await _http.GetAsync($"api/category/{categoryId}");
            var content=await category.Content.ReadAsStringAsync();
            if (!category.IsSuccessStatusCode) { 
                throw new Exception(content);
           
            }
            return await category.Content.ReadFromJsonAsync<CategoryDto>();
        }


        public async Task AddCategoryAsync(CreatecategoryDto category)
        {
            var newcategory = await _http.PostAsJsonAsync($"api/category",category );
            var content = await newcategory.Content.ReadAsStringAsync();
            if (!newcategory.IsSuccessStatusCode)
            {
                throw new Exception(content);

            }
        }
    }
}
