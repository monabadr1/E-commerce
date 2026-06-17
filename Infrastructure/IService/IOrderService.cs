using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared;

namespace Infrastructure.IService
{
    public interface IOrderService
    {
        Task<OrderDetailsDto?> GetOrderAsync(int orderId);

        Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId);

        Task UpdateOrderStatusAsync(int orderId, string status);

        Task ConfirmOrderAsync(int orderId);

        Task CollectOrderAsync(int orderId);

        Task<IEnumerable<OrderDto>> AdminGetOrdersAsync();

    }
}
