using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class OrderConsumer
    {
        private readonly HttpClient _http;

        public OrderConsumer(HttpClient http)
        {
            _http = http;
        }
        public async Task<OrderDetailsDto?> GetOrderAsync(int orderId)
        {
            var order = await _http.GetAsync($"api/orders/{orderId}");
            if (!order.IsSuccessStatusCode)
                return null;

            var data = await order.Content.ReadFromJsonAsync<OrderDetailsDto>();
            return data;

        }

        public async Task<List<OrderDto>?> GetOrdersAsync(int userId)
        {
            var order = await _http.GetAsync($"api/orders/user/{userId}");
            if (!order.IsSuccessStatusCode)
                return null;

            return await order.Content.ReadFromJsonAsync<List<OrderDto>>();

        }
    }
}
