using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Infrastructure.IService
{
    public interface IProductVariantService
    {
        Task<ProductVariantDto?> GetByIdAsync(int productvariantId);

        Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId);

        Task AddProductVariantAsync(CreateProductVariantDto productVariant);

        Task UpdateProductVariantAsync(ProductVariantDto productVariant);

        Task DeleteProductVariantAsync(int productVariantId);





    }
}
