using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {

        public int OrderItemId { get; set; }
        public int OrderId {  get; set; }

        public int ProductVariantId { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Order? Order { get; set; }

        public ProductVariant? ProductVariant { get; set; }


    }
}
