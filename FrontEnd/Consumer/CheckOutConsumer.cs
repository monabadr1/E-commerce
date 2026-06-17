using Shared;
using System.Net.Http.Json;

namespace FrontEnd.Consumer
{
    public class CheckOutConsumer
    {
        private readonly HttpClient _http;
        public CheckOutConsumer(HttpClient http)
        {
            _http = http;
        }

       public async Task CheckoutAsync(int userId, CheckoutDto dto)
        {
            var checkout = await _http.PostAsJsonAsync($"api/Checkout?userId={userId}", dto);
            var content=await checkout.Content.ReadAsStringAsync();
            if (!checkout.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }
    }
}
