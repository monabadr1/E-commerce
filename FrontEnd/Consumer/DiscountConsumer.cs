using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class DiscountConsumer
    {
        private readonly HttpClient _httpClient;

        public DiscountConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DiscountDto>> GetAllDiscountsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DiscountDto>>("api/discounts");
                return response ?? new List<DiscountDto>();
            }
            catch
            {
                return new List<DiscountDto>();
            }
        }

        public async Task<DiscountDto?> GetDiscountByIdAsync(int discountId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DiscountDto>($"api/discounts/{discountId}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateDiscountAsync(CreateDiscountDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/discounts", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ToggleDiscountStatusAsync(int discountId)
        {
            var response = await _httpClient.PatchAsync($"api/discounts/{discountId}/toggle-status", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDiscountAsync(int discountId)
        {
            var response = await _httpClient.DeleteAsync($"api/discounts/{discountId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<DiscountDto?> GetLatestActiveDiscountAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DiscountDto>("api/discounts/latest");
            }
            catch
            {
                return null;
            }
        }
    }

}
