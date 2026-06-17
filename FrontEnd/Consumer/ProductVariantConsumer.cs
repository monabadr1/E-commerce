using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class ProductVariantConsumer
    {
        private readonly HttpClient _http;
        public ProductVariantConsumer(HttpClient http)
        {
            _http = http;
        }
        public async Task<ProductVariantDto?>GetProductVariantById(int productvariantId)
        {
            var productvariant = await _http.GetAsync($"api/productvariant/{productvariantId}");
            var content=await productvariant.Content.ReadAsStringAsync();
            if (!productvariant.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await productvariant.Content.ReadFromJsonAsync<ProductVariantDto>();
        }

        public async Task<List<ProductVariantDto>?>GetByProductId(int productId)
        {
            var productvariant = await _http.GetAsync($"api/productvariant/product/{productId}");
            var content=await productvariant.Content.ReadAsStringAsync();
            if (!productvariant.IsSuccessStatusCode) { 
                throw new Exception(content);
            }
            return await productvariant.Content.ReadFromJsonAsync<List<ProductVariantDto>>();
        }
    }
}
