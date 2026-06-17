using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVariant
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }

        public string? Size { get; set; }

        public string? Color { set; get; }

        public int StockQuantity { get; set; }

        public decimal Price { get; set; }

        public Product? Product {  get; set; }

    }
}
