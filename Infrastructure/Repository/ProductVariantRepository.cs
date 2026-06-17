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
    public class ProductVariantRepository : IProductVariantRepository

    {
        private readonly AppDbContext _context;

        public ProductVariantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductVariant?> GetByIdAsync(int id)
        {
            return await _context.productVariants.FirstOrDefaultAsync(p => p.ProductVariantId == id);
        }

        public async Task<IEnumerable<ProductVariant>> GetByProductIdAsync(int productId)
        {
            return await _context.productVariants.Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task AddVariantAsync(ProductVariant variant)
        {
            await _context.productVariants.AddAsync(variant);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateVariantAsync(ProductVariant variant)
        {
            _context.productVariants.Update(variant);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVariantAsync(int id)
        {
            var variant = await _context.productVariants.FirstOrDefaultAsync(p => p.ProductVariantId == id);
            if (variant != null)
            {
                _context.productVariants.Remove(variant);
                await _context.SaveChangesAsync();
            }

        }
    }
}
