using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IDiscountRepository
    {
        Task<Discount> GetByIdAsync(int discountId);

        Task<IEnumerable<Discount>> GetAllAsync();

        Task AddAsync(Discount discount);

        void UpdateAsync(Discount discount);

        void DeleteAsync(Discount discount);

        Task AssignDiscountToProductsAsync(int discountId,List<int>productsIds);

        Task SaveChangesAsync();

        Task<Discount?> GetLatestActiveDiscountAsync();
    }
}
