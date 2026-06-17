using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
