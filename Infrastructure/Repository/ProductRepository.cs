using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            return await _context.products
                .Include(p=>p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.products
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .Include(p=>p.Reviews)
                .Include(p=>p.ProductVariants)
                .FirstOrDefaultAsync(p=>p.ProductId == id);
        }

        public async Task <IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.products
              .Where(p => p.CategoryId == categoryId)
              .Include(p=>p.ProductVariants)
              .Include(p=>p.ProductImages)
              .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByMainCategoryIdAsync(int mainCategoryId)
        {
            return await _context.products
                .Include(p => p.ProductVariants)
                .Include(p=>p.ProductImages)
                .Where(p => _context.categories.Any(c =>
                c.ParentCategoryId == mainCategoryId &&
                c.CategoryId == p.CategoryId)).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
             await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {

            var product= await _context.products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product != null) { 

                _context.products.Remove(product);
                await _context.SaveChangesAsync();
            }

        }

        public async Task UpdateAsync(Product product) { 

            _context.products.Update(product);
            await _context.SaveChangesAsync();

        }
    }
    }

