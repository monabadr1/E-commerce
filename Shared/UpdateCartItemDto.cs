using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UpdateCartItemDto
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }
        public int ProductVartiantId { get; set; }

        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }

        public string? Size { get; set; }
        public string? Color { get; set; }


        public int Quantity { get; set; }
    }
}
