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
    public class CartRepository:ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.carts.
                Include(c=>c.CartItems)
                .ThenInclude(ci=>ci.ProductVariant)
                .ThenInclude(pv=>pv.Product)
                .ThenInclude(p=>p.ProductImages)
                .FirstOrDefaultAsync(c => c.UserId == userId);

        }

        public async Task<CartItem?> GetCartItemAsync(int cartId, int producatvariantId )
        {
            return await _context.cartItems
                .FirstOrDefaultAsync(c=> c.CartId == cartId && c.ProductVariantId==producatvariantId );
        }


        public async Task CreateCartAsync(Cart cart)
        {
            await _context.carts.AddAsync(cart);
            await _context.SaveChangesAsync();

        }

        public async Task AddCartItemAsync(CartItem item)
        {
            await _context.cartItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

       public async Task UpdateCartItemAsync(CartItem item)
        {
            _context.cartItems.Update(item);
            await _context.SaveChangesAsync();
        }


        public async Task ClearCartAsync(int cartId)
        {
            var cart = await _context .cartItems
                .Where(c => c.CartId == cartId)
                .ToListAsync();
            if (cart.Any())
            {
                _context.cartItems.RemoveRange(cart);
                await _context.SaveChangesAsync();
            }
        }
        public async Task <CartItem?> GetCartItemByIdAsync(int CartItemId)
        {
            return await _context.cartItems
                .Include(ci=>ci.ProductVariant)
                .ThenInclude(c=>c.Product)
                .FirstOrDefaultAsync(c=>c.CartItemId==CartItemId);
        }
        public async Task RemoveItemAsync(int cartItemId)
        {
            var item = await _context.cartItems
                .FirstOrDefaultAsync(c => c.CartItemId == cartItemId);
                
            if (item != null)
            {
                _context.cartItems.Remove(item);
                await _context.SaveChangesAsync();

            }
        }
        
    }
}
