using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string? Name { get; set; } 

        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }
        public ICollection <Product> Product { get; set; }= new List<Product>();

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    }
}
