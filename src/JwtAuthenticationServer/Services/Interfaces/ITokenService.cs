using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;
public interface ITokenService
{
    Task<string> GenerateTokenAsync(UserData user);
}
