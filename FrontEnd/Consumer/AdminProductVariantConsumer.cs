using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class AdminProductVariantConsumer
    {
        private readonly HttpClient _http;

        public AdminProductVariantConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task AddProductVariantAsync(CreateProductVariantDto productvariant)
        {
            var variant = await _http.PostAsJsonAsync("api/admin/productvariant", productvariant);
            var content = await variant.Content.ReadAsStringAsync();
            if (!variant.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }
        public async Task UpdateProductvariant(int productvariantId, ProductVariantDto productVariant)
        {
            var variant = await _http.PutAsJsonAsync($"api/admin/productvariant/{productvariantId}", productVariant);
            var content = await variant.Content.ReadAsStringAsync();
            if (!variant.IsSuccessStatusCode)
            {
                throw new Exception(content);

            }
        }

        public async Task Deleteproductvariant(int productvariantId)
        {
            var variant = await _http.DeleteAsync($"api/admin/productvariant/{productvariantId}");
            var content = await variant.Content.ReadAsStringAsync();
            if (!variant.IsSuccessStatusCode)
            {
                throw new Exception(content);

            }
        }
    }
}
