using Shared;
using System.Net.Http.Json;
namespace FrontEnd.Consumer
{
    public class AuthConsumer
    {
        private readonly HttpClient _http;
        public AuthConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task <AuthResponseDto?>LoginAsync(LoginDto dto)

        {
            var res = await _http.PostAsJsonAsync("api/auth/login",dto);
            var content=await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
            return await res.Content.ReadFromJsonAsync<AuthResponseDto>();
            

        }
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/auth/register",dto);
            res.EnsureSuccessStatusCode();

            return await res.Content.ReadFromJsonAsync<AuthResponseDto>();
            
        }

    }
}
