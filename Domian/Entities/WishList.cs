using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WishList
    {
        public int WishListId{ get; set; }

        public int UserId {  get; set; }

        public User ? User { get; set; }

        public ICollection <WishListItem> WishListItems { get; set; }=new HashSet<WishListItem>();


    }
}
