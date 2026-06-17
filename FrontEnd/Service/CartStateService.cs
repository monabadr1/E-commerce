using Blazored.LocalStorage;
using Shared;

namespace FrontEnd.Service
{
    public class CartStateService
    {
        private readonly ILocalStorageService _localStorage;

        public CartStateService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        private const string Key = "cart";
        public async Task<List<LocalCartItemDto>> GetCartAsync()
        {
            return await _localStorage.GetItemAsync<List<LocalCartItemDto>>(Key)
                ?? new List<LocalCartItemDto>();
        }

        public async Task AddToCartAsync(LocalCartItemDto item)
        {
            var cart = await GetCartAsync();

            var existing = cart.FirstOrDefault(
                x => x.ProductVariantId == item.ProductVariantId
                );
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }
            await _localStorage.SetItemAsync(Key, cart);
        }

        public async Task RemoveItemAsync(int productVariantId)
        {
            var cart = await GetCartAsync();

            var item = cart.FirstOrDefault(
                x => x.ProductVariantId == productVariantId
                );
            if (item != null)
            {
                cart.Remove(item);
                await _localStorage.SetItemAsync(Key, cart);
            }
        }
        public async Task UpdateQuantityAsync(
    int productVariantId,
    int quantity)
        {
            var cart = await GetCartAsync();

            var item = cart.FirstOrDefault(
                x => x.ProductVariantId == productVariantId
            );

            if (item != null)
            {
                item.Quantity = quantity;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }

                await _localStorage.SetItemAsync(Key, cart);
            }
        }
        public async Task ClearCartAsync()
        {
            await _localStorage.RemoveItemAsync(Key);
        }
    }
}
