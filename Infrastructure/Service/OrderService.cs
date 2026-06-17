using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Infrastructure.Repository;

namespace Infrastructure.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository ,ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;

        }
 

       public async  Task<OrderDetailsDto?> GetOrderAsync(int orderId)
        {
           var order= await _orderRepository.GetByIdAsync(orderId); 
            if (order==null)
                return null;
            return new OrderDetailsDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Address = order.Address,
                City = order.City,
                Status = order.Status.ToString(),
                Phone = order.Phone,
                CreatedAt = order.CreatedAt,
                Items = order.OrderItems.Select(item => new OrderItemDetailsDto
                {
                    OrderItemId= item.OrderItemId,
                    ProductVariantId= item.ProductVariantId,
                    ProductId=item.ProductVariant?.ProductId??0,
                    ProductName=item.ProductVariant?.Product?.Name,
                    ImageUrl=item.ProductVariant?.Product?.ProductImages.FirstOrDefault()?.ImageUrl,
                    Size=item.ProductVariant?.Size,
                    Color=item.ProductVariant?.Color,
                    Price=item.Price,
                    Quantity=item.Quantity,
                }
                ).ToList()


            };

        }

       public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId)
        {
            var orders= await _orderRepository.GetByUserIdAsync(userId);

            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Address = order.Address,
                City = order.City,
                Status = order.Status.ToString(),
                Phone = order.Phone,
                CreatedAt = order.CreatedAt,

            }).ToList();

        }

       public async Task UpdateOrderStatusAsync(int orderId, string status) {

            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new Exception("Order not found ");


            if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                throw new Exception("Invalid order status");

            await _orderRepository.UpdateOrderStatusAsync(orderId, parsedStatus);
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("order not found");

            if (order.Status != OrderStatus.Processing)
                throw new Exception("only processing orders can be confirmed");
            await _orderRepository.UpdateOrderStatusAsync(orderId, OrderStatus.Confirmed);
        }

        public async Task CollectOrderAsync(int orderId) { 

            var order=await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("order not found");
            if (order.Status != OrderStatus.Confirmed &&
                order.Status != OrderStatus.Readyforcollection)
                throw new Exception("Order must be confirmed or ready for collection first");
            await _orderRepository.UpdateOrderStatusAsync(orderId, OrderStatus.collected);
        }
        public async Task<IEnumerable<OrderDto>> AdminGetOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId= o.UserId,
                Phone = o.Phone,
                TotalPrice = o.TotalPrice,
                City = o.City,
                CreatedAt= o.CreatedAt,
                Address = o.Address,
                Status  =o.Status.ToString(),

            }).ToList();
        }



    }
}
