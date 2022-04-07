using Awarean.Sdk.Result;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;
public interface ITokenService
{
    Task<string> GenerateTokenAsync(UserData user);
    Task<string> GenerateRefreshTokenAsync(UserData user);
    Task<AuthenticationTokens> GenerateAsync(UserData user);
    Task<Result> ValidateAsync(string token);

}
