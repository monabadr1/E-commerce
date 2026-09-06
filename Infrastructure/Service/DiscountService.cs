using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<DiscountDto> CreateDiscountAsync(CreateDiscountDto dto)
        {
            if (dto.Percentage <= 0 || dto.Percentage > 100)
                throw new ArgumentException("Discount percentage must be between 1% and 100%");

            if (dto.EndDate < dto.StartDate)
                throw new ArgumentException("End date cannot be earlier than start date");

            var discount = new Discount
            {
                Name = dto.Name,
                Percentage = dto.Percentage,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = true
            };

            await _discountRepository.AddAsync(discount);
            await _discountRepository.SaveChangesAsync();

            // ربط المنتجات المختارة بالخصم الجديد
            if (dto.SelectedProductIds != null && dto.SelectedProductIds.Any())
            {
                await _discountRepository.AssignDiscountToProductsAsync(discount.DiscountId, dto.SelectedProductIds);
                await _discountRepository.SaveChangesAsync();
            }

            return MapToDto(discount);
        }

        public async Task<IEnumerable<DiscountDto>> GetAllDiscountsAsync()
        {
            var discounts = await _discountRepository.GetAllAsync();
            return discounts.Select(MapToDto);
        }

        public async Task<DiscountDto?> GetDiscountByIdAsync(int discountId)
        {
            var discount = await _discountRepository.GetByIdAsync(discountId);
            return discount == null ? null : MapToDto(discount);
        }

        public async Task<bool> ToggleDiscountStatusAsync(int discountId)
        {
            var discount = await _discountRepository.GetByIdAsync(discountId);
            if (discount == null) return false;

            discount.IsActive = !discount.IsActive;
            _discountRepository.UpdateAsync(discount);
            await _discountRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDiscountAsync(int discountId)
        {
            var discount = await _discountRepository.GetByIdAsync(discountId);
            if (discount == null) return false;

            _discountRepository.DeleteAsync(discount);
            await _discountRepository.SaveChangesAsync();
            return true;
        }


        private static DiscountDto MapToDto(Discount discount)
        {
            return new DiscountDto
            {
                DiscountId = discount.DiscountId,
                Name = discount.Name,
                Percentage = discount.Percentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                IsActive = discount.IsActive,
                AppliedProductsCount = discount.Products?.Count ?? 0
            };
        }
       public async Task<DiscountDto?> GetLatestActiveDiscountAsync()
        {
            var discount =await _discountRepository.GetLatestActiveDiscountAsync();

            if (discount == null)
                return null;

            return new DiscountDto
            {
                DiscountId=discount.DiscountId,
                Name=discount.Name,
                Percentage=discount.Percentage,
                IsActive=discount.IsActive,
                EndDate=discount.EndDate,
                StartDate=discount.StartDate,
                AppliedProductsCount = discount.Products?.Count ?? 0

            };
        }


    }
}
