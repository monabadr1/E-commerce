using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class AdminOrderConsumer
    {
        private readonly HttpClient _http;
        public AdminOrderConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task  UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _http.PutAsync($"api/admin/order/{orderId}/status?status={status}",null);
            var content= await order.Content.ReadAsStringAsync();
            if (!order.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
 
        }

        public async Task CollectOrderAsync(int orderId)
        {
            var order = await _http.PostAsync($"api/admin/order/{orderId}/collect", null);
            var content = await order.Content.ReadAsStringAsync();
            if (!order.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var order = await _http.PostAsync($"api/admin/order/{orderId}/confirm",null);
            var content=await order.Content.ReadAsStringAsync();

            if (!order.IsSuccessStatusCode) { 
                throw new Exception(content);
            
            }
        }

        public async Task<List<OrderDto>?> AdminGetOrderAsync()
        {
            var order = await _http.GetAsync("api/admin/order");
            var content = await order.Content.ReadAsStringAsync();

            if (!order.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await order.Content.ReadFromJsonAsync<List<OrderDto>>();
        }

    }
}
