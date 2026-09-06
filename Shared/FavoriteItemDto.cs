using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class FavoriteItemDto
    {
        public int ProductId {  get; set; }
        public int ProductVariantId {  get; set; }

        public string? ProductName {  get; set; }

        public string? ImageUrl {  get; set; }

        public decimal Price {  get; set; }

        public decimal OriginalPrice { get; set; }
    }
}
