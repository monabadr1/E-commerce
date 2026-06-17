using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Infrastructure.Repository;
using Microsoft.Extensions.Caching.Memory;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;

        public CartService(ICartRepository cartRepository,IProductRepository prouductRepository , IProductVariantRepository productVariantRepository) { 
        _cartRepository = cartRepository;
        _productRepository = prouductRepository;
        _productVariantRepository = productVariantRepository;
        }


        public async Task<CartDto?> GetCartAsync(int userId)
        {
           var cart=  await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null) 
                return null;
            var items = cart.CartItems.Select(item => new CartItemDto
            {
                CartItemId = item.CartItemId,
                CartId = item.CartId,
                ProductVartiantId = item.ProductVariantId,
                ProductName = item.ProductVariant?.Product?.Name,
                ImageUrl = item.ProductVariant?.Product?.ProductImages.FirstOrDefault()?.ImageUrl,
                Size=item.ProductVariant?.Size,
                Color=item.ProductVariant?.Color,
                Price = item.Price,
                Quantity = item.Quantity,
            }
            ).ToList();

            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Items = items,
                TotalPrice=items.Sum(x=>x.Total)
            };
            
        }

        public async Task AddToCartAsync(int userId, int productVariantId, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            var variant = await _productVariantRepository.GetByIdAsync(productVariantId);
            if (variant == null)
                throw new Exception("variant not found");
            if (variant.StockQuantity < quantity)
                throw new Exception("stock is not found ");
            var cart=await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null) { 
                cart = new Cart {UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }
            var cartItem = await _cartRepository.GetCartItemAsync(cart.CartId, productVariantId);

            if (cartItem != null) {
                if (variant.StockQuantity < cartItem.Quantity + quantity)
                    throw new Exception("Not enough stock");
                cartItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);            
            }
            else
            {

                var item = new CartItem
                {
                    CartId=cart.CartId,
                    Quantity = quantity,
                    ProductVariantId=productVariantId,
                    Price = variant.Price

                };
                await _cartRepository.AddCartItemAsync(item);
            }
      
        }



        public async Task UpdateQuantityAsync(UpdateCartItemDto item)
        {
            if (item.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            var existingItem = await _cartRepository.GetCartItemByIdAsync(item.CartItemId);
            if (existingItem == null)
                throw new Exception("Cart item not found");
            var variant = await _productVariantRepository.GetByIdAsync(existingItem.ProductVariantId);
            if (variant == null)
                throw new Exception("Product variant not found");

            if (item.Quantity > variant.StockQuantity)
                throw new Exception("Not enough stock");

            existingItem.Quantity = item.Quantity;
            await _cartRepository.UpdateCartItemAsync(existingItem);
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                await _cartRepository.ClearCartAsync(cart.CartId);
            }
        
        }
        public async Task RemoveCartItemAsync(int userId,int cartItemId)
        {
            var cart =await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                throw new Exception("cart not found ");
            var item = cart.CartItems.FirstOrDefault(x=>x.CartItemId==cartItemId);

            if (item == null)
                throw new Exception("Item not found");
            await _cartRepository.RemoveItemAsync(cartItemId);
        }

        public async Task SyncCartAsync(int userId, List<LocalCartItemDto>localItems)
        {
            if (localItems == null || !localItems.Any())
                return;
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }
            foreach(var localItem in localItems)
            {
                var variant = await _productVariantRepository.GetByIdAsync(localItem.ProductVariantId);
                if (variant == null) continue; 

                var existingItem = await _cartRepository.GetCartItemAsync(cart.CartId, localItem.ProductVariantId);

                if (existingItem != null)
                {
                    existingItem.Quantity += localItem.Quantity;
                    if (existingItem.Quantity > variant.StockQuantity)
                        existingItem.Quantity = variant.StockQuantity; 

                    await _cartRepository.UpdateCartItemAsync(existingItem);
                }
                else
                {
                    var newItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductVariantId = localItem.ProductVariantId,
                        Quantity = localItem.Quantity > variant.StockQuantity ? variant.StockQuantity : localItem.Quantity,
                        Price = variant.Price
                    };
                    await _cartRepository.AddCartItemAsync(newItem);
                }
            }
        }

        }

    }

