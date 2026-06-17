
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Infrastructure.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync();
        Task<ProductDetailsDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);

        Task<IEnumerable<ProductDto>>GetProductsByMainCategoryAsync(int mainCategoryId);
        Task  AddProductAsync(CreateProductDto product);

        Task UpdateProductAsync(ProductDto product);
        Task DeleteProductAsync(int id);
    }
}
