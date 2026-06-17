using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class WishListConsumer
    {
        private readonly HttpClient _http;

        public WishListConsumer(HttpClient http) { 
            _http = http;
        
        }

        public async Task AddWishListAsync(int productVariantId)
        {
            var wishlist = await _http.PostAsync($"api/wishlist/items?productVariantId={productVariantId}",null);
            var content=await wishlist.Content.ReadAsStringAsync();
            if (!wishlist.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task DeletItemWishListAsync(int wishlistItemId)
        {
            var wishlist = await _http.DeleteAsync($"api/wishlist/items/{wishlistItemId}");
            var content = await wishlist.Content.ReadAsStringAsync();
            if (!wishlist.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task<WishListDto?> GetWishListByUserIdAsync()
        {
            var wishlist = await _http.GetAsync("api/wishlist");
            var content=await wishlist.Content.ReadAsStringAsync();
            if (!wishlist.IsSuccessStatusCode)
            {
               Console.WriteLine(content);
                throw new Exception($"Status:{wishlist.StatusCode}-{content}");
            }

            return await wishlist.Content.ReadFromJsonAsync<WishListDto>();
        }

        public async Task<WishListItemDto?> GetWishListItemAsync(int wishlistiemId)
        {
            var wishlist = await _http.GetAsync($"api/wishlist/items/{wishlistiemId}");
            var content = await wishlist.Content.ReadAsStringAsync();
            if (!wishlist.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            return await wishlist.Content.ReadFromJsonAsync<WishListItemDto>();
        }
        public async Task SyncWishListAsync(List<FavoriteItemDto> favoriteItems)
        {
            var res = await _http.PostAsJsonAsync("api/wishlist/sync", favoriteItems);
            if (!res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                throw new Exception($"Sync failed: {content}");
            }
        }
        public async Task ClearCartAsync()
        {
            var wishlist = await _http.DeleteAsync("api/wishlist");
            var content = await wishlist.Content.ReadAsStringAsync();
            if (!wishlist.IsSuccessStatusCode)
            {
                throw new Exception(content);

            }
        }
    }
}
