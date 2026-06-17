using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public  class CartDto
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public List<CartItemDto> Items { get; set; } = new();

        public decimal TotalPrice {  get; set; }

    }
}
