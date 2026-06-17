using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.IRepository
{
    public interface IWishlistRepository
    {
        Task CreateWishList(WishList wishList);
        Task AddWishListItem(WishListItem wishListItem);

        Task DeleteWishList(int wishlistItemId);
        Task<WishList?> GetWishListByUserId(int userId);

        Task<WishListItem?> GetWishListItem(int wishlistItemId);
        Task ClearWishList(int wishlistId);


    }
}
