using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class WishListDto
    {
        public int WishListId { get; set; }

        public int UserId { get; set; }
        public ICollection<WishListItemDto> wishListItems { get; set; } = new HashSet<WishListItemDto>();

    }
}
