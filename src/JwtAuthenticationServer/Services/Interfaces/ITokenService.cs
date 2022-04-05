using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;
public interface ITokenService
{
    string GenerateToken(UserData user);
    string GenerateRefreshToken(UserData user);
    AuthenticationTokens Generate(UserData user);
}
