using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services
{
    public interface ITokenRepository
    {
        Task<string> GetToken(User user);
        Task<string> GetRefreshToken(User user);
        Task SaveToken(AuthenticationTokens tokens);
        Task<bool> IsActiveAsync(string token);
        Task RefreshToken(string token);
        Task ExpireToken(User user);
    }
}