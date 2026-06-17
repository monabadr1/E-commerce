using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
    public interface IwishlistService
    {
        Task AddWishListAsync(int userId, int productVariantId);

        Task DeletItemWishListAsync(int wishlistItemId);

        Task<WishListDto> GetWishListByUserIdAsync(int userId);

        Task <WishListItemDto>GetWishListItemAsync(int wishlistiemId);
        Task SyncWishListAsync(int userId, List<FavoriteItemDto> favoriteItems);

        Task ClearWishlistAsync(int userId);


    }
}
