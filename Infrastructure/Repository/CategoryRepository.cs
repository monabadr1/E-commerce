using Domain.Entities;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository (AppDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.categories.ToListAsync();
        }

       public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }
       public async Task AddAsync(Category category)
        {
            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

       public async Task DeleteAsync(int id)
        {
           var category= await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category != null)
            {
                _context.categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
       public async Task UpdateAsync(Category category)
        {
            _context.categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task <IEnumerable<Category>> GetMainCategoriesAsync()
        {
            return await _context.categories
                .Where(c=>c.ParentCategoryId==null)
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>>GetSubCategoriesAsync(int parentCategoryId)
        {
            return await _context.categories
                .Where(c=>c.ParentCategoryId== parentCategoryId)
                .ToListAsync();
        }
    }
}
