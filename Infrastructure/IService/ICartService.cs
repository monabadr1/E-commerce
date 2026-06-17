using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
namespace Infrastructure.IService
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync(int userId);

        Task AddToCartAsync(int userId, int productvariantId, int quantity);


        Task UpdateQuantityAsync(UpdateCartItemDto item);

        Task ClearCartAsync(int userId);
        Task RemoveCartItemAsync(int userId,int cartItemId);
        Task SyncCartAsync(int userId, List<LocalCartItemDto> localItems);

    }
}
