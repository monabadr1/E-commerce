using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class AdminProductConsumer
    {
        private readonly HttpClient _http;
        public AdminProductConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task AddProductAsync(CreateProductDto createProduct)
        {
            var product = await _http.PostAsJsonAsync("api/admin/products", createProduct);
            var content = await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task UpdateProductAsync(int productId, ProductDto productDto)
        {
            var product = await _http.PutAsJsonAsync($"api/admin/products/{productId}", productDto);
            var content = await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _http.DeleteAsync($"api/admin/products/{productId}");
            var content=await product.Content.ReadAsStringAsync();
            if (!product.IsSuccessStatusCode) { 
                throw new Exception(content);
            }

        }

    }
}
