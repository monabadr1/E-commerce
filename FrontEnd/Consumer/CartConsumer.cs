using Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class CartConsumer
    {
        private readonly HttpClient _http;

        public CartConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task<CartDto?> GetCartAsync()
        {
            var response = await _http.GetAsync("api/cart");

            if (!response.IsSuccessStatusCode)
            {
                return new CartDto();
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent ||
                response.Content.Headers.ContentLength == 0)
            {
                return new CartDto(); 
            }

            // 3. Safe to deserialize now
            return await response.Content.ReadFromJsonAsync<CartDto>();
        }

        public async Task AddToCartAsync( int productvariantId, int quantity)
        {
            var cart = await _http.PostAsync($"api/cart?productvariantId={productvariantId}&quantity={quantity}", null);
            var content = await cart.Content.ReadAsStringAsync();
            if (!cart.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

        }


        public async Task UpdateQuantityAsync(UpdateCartItemDto item)
        {
            var cart = await _http.PutAsJsonAsync("api/cart/item", item);
            var content = await cart.Content.ReadAsStringAsync();
            if (!cart.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task ClearCartAsync()
        {
            var cart = await _http.DeleteAsync("api/cart");
            var content = await cart.Content.ReadAsStringAsync();
            if (!cart.IsSuccessStatusCode)
            {
                throw new Exception(content);

            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cart = await _http.DeleteAsync($"api/cart/item/{cartItemId}");
            var content= await cart.Content.ReadAsStringAsync();
            if (!cart.IsSuccessStatusCode) { 
                throw new Exception(content);
            }
        }
        public async Task SyncCartAsync(List<LocalCartItemDto> localItems)
        {
            var res = await _http.PostAsJsonAsync("api/cart/sync", localItems);
            if (!res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                throw new Exception($"Sync failed: {content}");
            }
        }
    }
}
