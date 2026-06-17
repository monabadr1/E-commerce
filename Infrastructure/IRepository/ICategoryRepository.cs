using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<IEnumerable<Category>> GetMainCategoriesAsync();

        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentCategoryId);

        Task <Category?> GetByIdAsync(int id);
        Task AddAsync (Category category);
        Task DeleteAsync (int id);
        Task UpdateAsync (Category category);
    }
}
