using LAMAMedellin.Client.Models;

namespace LAMAMedellin.Client.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
    }
}