using Blazored.LocalStorage;
using LAMAMedellin.Client.Models;
using System.Net.Http.Json;

namespace LAMAMedellin.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/login", request);
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (result?.Success == true && result.Token != null)
                {
                    await _localStorage.SetItemAsStringAsync(TokenKey, result.Token);
                }

                return result ?? new AuthResponse { Success = false, Message = "Error al procesar la respuesta" };
            }
            catch (Exception ex)
            {
                return new AuthResponse { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/register", request);
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (result?.Success == true && result.Token != null)
                {
                    await _localStorage.SetItemAsStringAsync(TokenKey, result.Token);
                }

                return result ?? new AuthResponse { Success = false, Message = "Error al procesar la respuesta" };
            }
            catch (Exception ex)
            {
                return new AuthResponse { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsStringAsync(TokenKey);
        }
    }
}