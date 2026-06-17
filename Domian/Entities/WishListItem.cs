using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WishListItem
    {
        public int WishListItemId { get; set; }
        
        public int WishListId { get; set; }
        public int ProductvariantId {  get; set; }

        public WishList? WishList { get; set; }
        public ProductVariant?  ProductVariant { get; set; }

    }
}
