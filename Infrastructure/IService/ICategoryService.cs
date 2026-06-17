using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Infrastructure.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<IEnumerable<CategoryDto>> GetMainCategoryAsync();

        Task<IEnumerable<CategoryDto>> GetSubCategoryAsync(int parentCategory);

        Task<CategoryDto?> GetByIdCategoryAsync(int Categoryid);
        Task AddCategoryAsync(CreatecategoryDto category);
        Task DeletCategoryAsync(int id);
        Task UpdateCategoryAsync(CategoryDto category);



    }
}
