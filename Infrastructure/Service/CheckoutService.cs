using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Microsoft.Identity.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class CheckoutService:ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        public CheckoutService(ICartRepository cartRepository, IOrderRepository orderRepository,IProductVariantRepository productVariantRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productVariantRepository = productVariantRepository;
        }
      
        public async Task CheckoutAsync(int userId, CheckoutDto dto)
        {
            var cart=await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
                throw new Exception("cart is empty ");
            if (cart.CartItems == null)
                throw new Exception("cart has no items");

            decimal totelprice = 0;
            foreach (var items in cart.CartItems)
            {
                var variant = await _productVariantRepository.GetByIdAsync(items.ProductVariantId);
                if (variant == null)
                    throw new Exception("product variant not found");
                if (items.Quantity > variant.StockQuantity)
                    throw new Exception("product is unavalible");

                totelprice += items.Quantity * items.Price;

            }
            var order = new Order
            {
                UserId = userId,
                Phone = dto.Phone,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = totelprice,
                Status = OrderStatus.Processing,
                City=dto.City

            };
            await _orderRepository.AddOrderAsync(order);

            foreach(var items in cart.CartItems)
            {
                var variant = await _productVariantRepository.GetByIdAsync(items.ProductVariantId);
                if (variant == null)
                    throw new Exception("product variant not found");
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    Price = items.Price,
                    ProductVariantId = items.ProductVariantId,
                    Quantity = items.Quantity,
                };

                await _orderRepository.AddOrderItemAsync(orderItem);

                variant.StockQuantity -= items.Quantity;
                await _productVariantRepository.UpdateVariantAsync(variant);
            }
            await _cartRepository.ClearCartAsync(cart.CartId);

        }

    }
}
