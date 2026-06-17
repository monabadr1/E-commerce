using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);

        Task<CartItem?> GetCartItemAsync(int cartId, int productvariantId);

        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);

        Task CreateCartAsync(Cart cart);

        Task AddCartItemAsync(CartItem item);

        Task UpdateCartItemAsync(CartItem item);

        Task ClearCartAsync(int cartId);
        Task RemoveItemAsync(int cartItemId);

    }
}
