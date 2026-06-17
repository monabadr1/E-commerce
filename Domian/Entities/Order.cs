using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int OrderId {  get; set; }
        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Address { get; set; }
        
        public string? City { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Processing;

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public User? User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();

        public Payment?payment { get; set; }
    
    }

    public enum OrderStatus
    {
        Processing=0,
        Confirmed =1,
        Readyforcollection=2,
        collected=3
    }
}
