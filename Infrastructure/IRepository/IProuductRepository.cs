using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<IEnumerable<Product>>GetByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Product>>GetByMainCategoryIdAsync(int  mainCategoryId);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);

        Task DeleteAsync(int id); 

    }
}
