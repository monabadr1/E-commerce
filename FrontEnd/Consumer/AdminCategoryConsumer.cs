using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class AdminCategoryConsumer
    {
        private readonly HttpClient _http;
        public AdminCategoryConsumer(HttpClient http)
        {
            _http = http;

        }
        public async Task AddCategories(CreatecategoryDto categoryDto)
        {
            var category = await _http.PostAsJsonAsync("api/admin/category",categoryDto);
            var content=await category.Content.ReadAsStringAsync();
            if (!category.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }


        }

        public async Task updatecategory(int categoryId, CategoryDto categoryDto)
        {
            var category = await _http.PutAsJsonAsync($"api/admin/category/{categoryId}", categoryDto);
            var content = await category.Content.ReadAsStringAsync();
            if (!category.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task DeleteCategory(int categoryId) {

            var category = await _http.DeleteAsync($"api/admin/category/{categoryId}");
            var content = await category.Content.ReadAsStringAsync();
            if (!category.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }


        }
    }
}
