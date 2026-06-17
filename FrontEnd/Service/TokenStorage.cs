using Blazored.LocalStorage;

namespace FrontEnd.Service
{
    public class TokenStorage
    {
        private const string Key = "auth_token";
        private readonly ILocalStorageService _localStorageService;

        public TokenStorage(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task SetTokenAsync(string token)
        {
            token =(token??"").Trim();
            await _localStorageService.SetItemAsync(Key, token);

        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorageService.GetItemAsync<string>(Key);
        }
        public async Task RemoveTokenAsync()
        {
            await _localStorageService.RemoveItemAsync(Key);
        }
    }
}
