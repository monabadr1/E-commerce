using Domain.Entities;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class WishListRepository:IWishlistRepository
    {
        private readonly AppDbContext _context;
        public WishListRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task CreateWishList(WishList wishList)
        {
            await _context.wishLists.AddAsync(wishList);
            await _context.SaveChangesAsync();
        }
        public async Task AddWishListItem(WishListItem wishListItem)
        {
            await _context.wishListItems.AddAsync(wishListItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWishList(int wishlistItemId)
        {
            var wishlist= await _context.wishListItems
                .Where(w=>w.WishListItemId==wishlistItemId)
                .ToListAsync();
            if (wishlist.Any())
            {
                 _context.wishListItems.RemoveRange(wishlist);
                await _context.SaveChangesAsync();
            }

        }
        public async Task<WishList?> GetWishListByUserId(int userId)
        {
            return await _context.wishLists
                .Include(w => w.WishListItems)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Product)
                            .ThenInclude(p => p.ProductImages)
                .Include(w => w.WishListItems)
                    .ThenInclude(i => i.ProductVariant)
                        .ThenInclude(v => v.Product)
                            .ThenInclude(p => p.Discount)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<WishListItem?> GetWishListItem(int wishlistItemId)
        {
            return await _context.wishListItems
                .FirstOrDefaultAsync(w => w.WishListItemId == wishlistItemId);


        }
        public async Task ClearWishList(int wishlistId)
        {
            var wishlist = await _context.wishListItems
                .Where(c => c.WishListId == wishlistId)
                .ToListAsync();
            if (wishlist.Any())
            {
                _context.wishListItems.RemoveRange(wishlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
