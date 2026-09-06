using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly AppDbContext _context; 

        public CheckoutService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IProductVariantRepository productVariantRepository,
            AppDbContext context)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productVariantRepository = productVariantRepository;
            _context = context;
        }

        public async Task CheckoutAsync(int userId, CheckoutDto dto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty or has no items.");

            decimal totalPrice = 0;

            foreach (var item in cart.CartItems)
            {
                var variant = await _productVariantRepository.GetByIdAsync(item.ProductVariantId);
                if (variant == null)
                    throw new Exception($"Product variant with ID {item.ProductVariantId} not found.");

                if (item.Quantity > variant.StockQuantity)
                    throw new Exception($"Product '{variant.Product?.Name}' is out of stock or insufficient quantity.");

                totalPrice += item.Quantity * item.Price;
            }

            var order = new Order
            {
                UserId = userId,
                Phone = dto.Phone,
                Address = dto.Address,
                City = dto.City,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = totalPrice,
                Status = OrderStatus.Processing
            };

            await _orderRepository.AddOrderAsync(order);

            var payment = new Payment
            {
                OrderId = order.OrderId,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentMethod == "CashOnDelivery" ? "Pending" : "Completed",
                PaidAt = dto.PaymentMethod == "CashOnDelivery" ? null : DateTime.UtcNow,
                TransactionId = dto.PaymentMethod == "CashOnDelivery"
                    ? null
                    : $"TXN-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
            };

            await _context.payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            foreach (var item in cart.CartItems)
            {
                var variant = await _productVariantRepository.GetByIdAsync(item.ProductVariantId);

                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    Price = item.Price,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                };

                await _orderRepository.AddOrderItemAsync(orderItem);

                variant!.StockQuantity -= item.Quantity;
                await _productVariantRepository.UpdateVariantAsync(variant);
            }

            await _cartRepository.ClearCartAsync(cart.CartId);
        }
    }
}