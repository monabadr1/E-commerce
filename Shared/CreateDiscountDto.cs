using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CreateDiscountDto
    {
        public string Name { get; set; } = string.Empty;

        public decimal Percentage {  get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public List<int> SelectedProductIds { get; set; } = new();
    }
}
