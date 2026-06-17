using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public  class OrderItemDetailsDto
    {
        public int OrderItemId { get; set; }
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }

        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }

        public string? Size { get; set; }
        public string? Color { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
