using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductVariantDto
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }

        public string? size { get; set; }

        public string? color { set; get; }

        public int StockQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
