using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }
       public string? CategoryName { get; set; }

        public List<string> Images { get; set; } = new();

        public decimal Price { get; set; }

        public List<string> Colors { get; set; } = new();

        public decimal OriginalPrice {  get; set; }

        public decimal DiscountPercentage {  get; set; }

        



    }
}
