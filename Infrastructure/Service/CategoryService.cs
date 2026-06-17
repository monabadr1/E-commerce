using Domain.Entities;
using Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Infrastructure.IRepository;
using Microsoft.Identity.Client;

namespace Infrastructure.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            var categorys= await _categoryRepository.GetAllAsync();
            return categorys.Select(c=> new CategoryDto
            {
                Name = c.Name,
                CategoryId = c.CategoryId,
                ParentCategoryId = c.ParentCategoryId,
            }
                ).ToList();

        }
        public async Task<IEnumerable<CategoryDto>> GetMainCategoryAsync()
        {
            var categories = await _categoryRepository.GetMainCategoriesAsync();
            return categories.Select(c => new CategoryDto
            {
                CategoryId= c.CategoryId,
                Name = c.Name,
                ParentCategoryId= c.ParentCategoryId,

            }).ToList();
        }

        public async Task <IEnumerable<CategoryDto>> GetSubCategoryAsync(int parentcategoryId)
        {
            var categories=await _categoryRepository.GetSubCategoriesAsync(parentcategoryId);
            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                ParentCategoryId = c.ParentCategoryId,


            }

                ).ToList();
        }

        public async Task<CategoryDto?> GetByIdCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category==null)
                return null;
            return new CategoryDto
            {
                Name = category.Name,
                CategoryId = category.CategoryId,
                ParentCategoryId = category.ParentCategoryId,
            };
        }
       public async Task AddCategoryAsync(CreatecategoryDto category)
        {
            if (category.ParentCategoryId == 0)
                category.ParentCategoryId = null;

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new Exception("category name is required");
            if (category.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(category.ParentCategoryId.Value);
                if (parentCategory == null)
                    throw new Exception("Parent Categor not found ");
            }
            var entity = new Category
            {
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };
            await _categoryRepository.AddAsync(entity);
            
        }
        public async Task DeletCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("category not found");
            await _categoryRepository.DeleteAsync(id);
        }
       public async Task UpdateCategoryAsync(CategoryDto category)
        {
            var categories = await _categoryRepository.GetByIdAsync(category.CategoryId);
            if (categories == null)
                throw new Exception("category not found ");
            if (category.ParentCategoryId.HasValue)
            {
                if (category.ParentCategoryId.Value == category.CategoryId)
                    throw new Exception("Category cannot be parent of itself");

                var parentCategory = await _categoryRepository.GetByIdAsync(category.ParentCategoryId.Value);
                if (parentCategory == null)
                    throw new Exception("parent category not found");
            }

           categories.Name = category.Name;
           categories.ParentCategoryId = category.ParentCategoryId;
           await _categoryRepository.UpdateAsync(categories);
        }

    }
}
