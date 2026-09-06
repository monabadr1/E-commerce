using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _context;

        public DiscountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Discount?> GetByIdAsync(int discountId)
        {
            return await _context.discounts
                .Include(d => d.Products)
                .FirstOrDefaultAsync(d => d.DiscountId == discountId);

        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _context.discounts
                .Include(d => d.Products)
                .ToListAsync();
        }
        public async Task AddAsync(Discount discount)
        {
            await _context.discounts.AddAsync(discount);
        }

        public void UpdateAsync(Discount discount)
        {
            _context.discounts.Update(discount);
        }

        public void DeleteAsync(Discount discount)
        {
            _context.discounts.Remove(discount);

        }
        public async Task AssignDiscountToProductsAsync(int discountId, List<int> productIds)
        {
            if (productIds == null || !productIds.Any()) return;

            var productsToUpdate = await _context.products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            foreach (var product in productsToUpdate)
            {
                product.DiscountId = discountId;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Discount?> GetLatestActiveDiscountAsync()
        {
            return await _context.discounts
                .Include(d=>d.Products)
                .Where(d => d.IsActive)
                .OrderByDescending(d => d.DiscountId)
                .FirstOrDefaultAsync();
        }


    }
}
