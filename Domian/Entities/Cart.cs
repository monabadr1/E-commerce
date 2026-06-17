using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public int CartId {  get; set; }

        public int UserId {  get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public User? User { get; set; }
    }
}
