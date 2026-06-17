using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

        public ICollection<Discount> Discounts { get; set; }=new List<Discount>();
    }
}
