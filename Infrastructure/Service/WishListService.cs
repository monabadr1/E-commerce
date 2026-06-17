using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Infrastructure.Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class WishListService:IwishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        public WishListService(IWishlistRepository wishlistRepository, IProductVariantRepository productVariantRepository)
        {
            _wishlistRepository = wishlistRepository;
            _productVariantRepository = productVariantRepository;
        }
        public async Task AddWishListAsync(int userId, int productVariantId)
        {
            var variant = await _productVariantRepository.GetByIdAsync(productVariantId);
            if (variant == null)
                throw new Exception("variant not found");
            if (variant.StockQuantity <= 0)
                throw new Exception("product is not have stock");

            var wishlist = await _wishlistRepository.GetWishListByUserId(userId);
            if (wishlist == null)
            {
                wishlist = new WishList{UserId = userId};
                await _wishlistRepository.CreateWishList(wishlist);
            }

            var alreadyexist = wishlist.WishListItems
                .Any(x => x.ProductvariantId == productVariantId);
            if (alreadyexist)
            {
                throw new Exception("product already exists in wishlist");
            }
            var entity = new WishListItem
            {
                ProductvariantId = productVariantId,
                WishListId=wishlist.WishListId,
            };

            await _wishlistRepository.AddWishListItem(entity);
                

        }


       public async Task DeletItemWishListAsync(int wishlistitemId)
        {
            var wishlistItem = await _wishlistRepository.GetWishListItem(wishlistitemId);
            if (wishlistItem == null)
                throw new Exception("wishlistItem not found");
            await _wishlistRepository.DeleteWishList(wishlistitemId);

        }

        public async Task<WishListDto> GetWishListByUserIdAsync(int userId)
        {
            var wishlist = await _wishlistRepository.GetWishListByUserId(userId);
            if (wishlist == null)
            {
                wishlist = new WishList
                {
                    UserId = userId
                };

                await _wishlistRepository.CreateWishList(wishlist);
            }

            return new WishListDto
            {
                UserId = userId,
                WishListId = wishlist.WishListId,
                wishListItems = wishlist.WishListItems.Select(item => new WishListItemDto
                {
                    ProductvariantId = item.ProductvariantId,
                    WishListId=item.WishListId,
                    WishListItemId=item.WishListItemId,
                    Price=item.ProductVariant?.Price ?? 0,
                    ProductName=item.ProductVariant?.Product?.Name,
                    ImageUrl=item.ProductVariant?
                    .Product?
                    .ProductImages?
                    .FirstOrDefault()?.ImageUrl
                    

                }).ToList()

            };

        }

       public async Task<WishListItemDto> GetWishListItemAsync(int wishlistiemId)
        {
            var wishlistItem = await _wishlistRepository.GetWishListItem(wishlistiemId);
            if (wishlistItem == null)
                throw new Exception("wishlistItem is empty");
            return new WishListItemDto
            {
                ProductvariantId = wishlistItem.ProductvariantId,
                WishListId = wishlistItem.WishListId,
                WishListItemId = wishlistItem.WishListItemId,
            };


        }
        public async Task SyncWishListAsync(int userId, List<FavoriteItemDto> favoriteItems)
        {
            if (favoriteItems == null || !favoriteItems.Any())
                return;
            var wishlist=await _wishlistRepository.GetWishListByUserId(userId);
            if (wishlist == null)
            {
                wishlist = new WishList { UserId = userId ,WishListItems=new List<WishListItem>()};
                await _wishlistRepository.CreateWishList(wishlist);
            }
            if (wishlist.WishListItems == null)
            {
                wishlist.WishListItems = new List<WishListItem>();
            }

            foreach (var favoritItem in favoriteItems)
            {
                var variant=await _productVariantRepository.GetByIdAsync(favoritItem.ProductVariantId);
                if (variant == null)
                    continue;
                var existingItem = wishlist.WishListItems.Any(x => x.ProductvariantId == favoritItem.ProductVariantId);
                if (!existingItem )
                {
                    var newItem = new WishListItem
                    {
                        WishListId = wishlist.WishListId,
                        ProductvariantId = variant.ProductVariantId,

                    };
                    await _wishlistRepository.AddWishListItem(newItem);

                }


            }
        }
        public async Task ClearWishlistAsync(int userId)
        {
            var wishList = await _wishlistRepository.GetWishListByUserId(userId);
            if (wishList != null)
            {
                await _wishlistRepository.ClearWishList(wishList.WishListId);
            }

        }

    }
}
