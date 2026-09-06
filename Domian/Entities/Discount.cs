using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Discount
    {
        public int DiscountId {  get; set; }

        public string? Name {  get; set; }

        public decimal Percentage {  get; set; }

        public DateOnly StartDate {  get; set; }

        public DateOnly EndDate { get; set; }

        public bool IsActive { get; set; }

        public int? ProductId {  get; set; }

        public ICollection<Product> Products { get; set; }=new List<Product>();
    }
}
