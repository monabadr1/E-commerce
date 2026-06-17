using Blazored.LocalStorage;
using Shared;

namespace FrontEnd.Service
{
    public class FavoriteStateService
    {
        private readonly ILocalStorageService _localStorage;

        public FavoriteStateService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        private const string Key = "favorites";
        public async Task<List<FavoriteItemDto>> GetFavoriteAsync()
        {
            return await _localStorage.GetItemAsync<List<FavoriteItemDto>>(Key)
                ?? new List<FavoriteItemDto>();
        }

        public async Task AddToFavoriteAsync(FavoriteItemDto item)
        {
            var favorites = await GetFavoriteAsync();

            if (!favorites.Any(x => x.ProductVariantId == item.ProductVariantId))
            {
                favorites.Add(item);
                await _localStorage.SetItemAsync(Key, favorites);
            }
        }
        public async Task RemoveFromFavoriteAsync(int productVariantId)
        {
            var favorites=await GetFavoriteAsync();

            favorites.RemoveAll(x => x.ProductVariantId == productVariantId);
            await _localStorage.SetItemAsync(Key, favorites);
        }
        public async Task<bool> IsFavoriteAsync(int productVariantId)
        {
            var favorites=await GetFavoriteAsync();
            return favorites.Any(x => x.ProductVariantId == productVariantId);
        }

        public async Task ClearFavoriteAsync()
        {
            await _localStorage.RemoveItemAsync(Key);
        }
    }
}
