using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OrderItemDto> Items { get; set; } = new();

    }
}
