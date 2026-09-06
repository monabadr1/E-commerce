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
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.orders
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.ProductVariant)
                .ThenInclude(o=>o.Product)
                .ThenInclude(p=>p.ProductImages)
                .Include(u=>u.User)
                .Include(o=>o.payment)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task <IEnumerable<Order>> GetAllAsync()
        {
            return await _context.orders
                .Include(o => o.User)
                .Include(o=>o.OrderItems)
                .Include(o=>o.payment)
                .ToListAsync();
        }
      public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            return await _context.orders
                 .Where(o => o.UserId == userId)
                .ToListAsync();
        }

       public async Task<int> AddOrderAsync(Order order)
        {
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task AddOrderItemAsync(OrderItem item)
        {
            await _context.orderItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

       public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            return await _context.orderItems
                .Where(o=>o.OrderId==orderId)
                .Include(o=>o.ProductVariant)
                .ToListAsync(); 
        }

       public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _context.orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Status = status;
            await _context.SaveChangesAsync();
            
            

     
        }

    }
}
