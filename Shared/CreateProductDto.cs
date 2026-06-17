using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }


        public List<string> Images { get; set; } = new();
    }
}
