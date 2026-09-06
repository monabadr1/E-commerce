using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
    public interface IDiscountService
    {
        Task<DiscountDto> CreateDiscountAsync(CreateDiscountDto dto);
        Task<IEnumerable<DiscountDto>> GetAllDiscountsAsync();
        Task<DiscountDto?> GetDiscountByIdAsync(int discountId);
        Task<bool> ToggleDiscountStatusAsync(int discountId);
        Task<bool> DeleteDiscountAsync(int discountId);
        Task<DiscountDto?> GetLatestActiveDiscountAsync();

    }
}
