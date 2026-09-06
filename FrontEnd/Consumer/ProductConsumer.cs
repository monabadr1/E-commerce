using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class ProductConsumer
    {
        private readonly HttpClient _http;
        public ProductConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task <List<ProductDto>?> GetProductAsync()
        {
            var product = await _http.GetAsync("api/products");
            var content=await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await product.Content.ReadFromJsonAsync<List<ProductDto>>();
        }

        public async Task<ProductDetailsDto?> GetProductByIdAsync(int productId)
        {
            var product = await _http.GetAsync($"api/products/{productId}");
            var content=await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode) {
                throw new Exception(content);
            }
            return await product.Content.ReadFromJsonAsync<ProductDetailsDto>();
        }

        public async Task<List<ProductDto>?> GetproductByCategoryIdAsync(int categoryId)
        {
            var product = await _http.GetAsync($"api/products/category/{categoryId}");
            var content= await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode)
            {
                throw new Exception(content);
                
            }
            return await product.Content.ReadFromJsonAsync<List<ProductDto>>();
        }

        public async Task<List<ProductDto>?> GetProductsByMainCategoryAsync(int maincategoryId)
        {
            var product = await _http.GetAsync($"api/products/maincategory/{maincategoryId}");
            var content = await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await product.Content.ReadFromJsonAsync<List<ProductDto>>();
        }
    }
}
