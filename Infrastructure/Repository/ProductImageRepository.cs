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
    public class ProductImageRepository :IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _context.productImages
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
        public async Task AddAsync(ProductImage productImage) 
        {
            await _context.productImages.AddAsync(productImage);

            await _context.SaveChangesAsync();
        
        }
    }
}
