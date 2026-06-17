using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.IRepository
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);

        Task<IEnumerable<Order>> GetAllAsync();
        Task<int> AddOrderAsync(Order order);

        Task AddOrderItemAsync(OrderItem item);

        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);

        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}
