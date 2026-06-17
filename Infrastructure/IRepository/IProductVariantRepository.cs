using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.IRepository
{
    public interface IProductVariantRepository
    {
        Task <ProductVariant?> GetByIdAsync (int id);

        Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId);

        Task AddVariantAsync(ProductVariant variant);
        Task UpdateVariantAsync (ProductVariant variant);
        Task DeleteVariantAsync (int id);
        
    }
}
