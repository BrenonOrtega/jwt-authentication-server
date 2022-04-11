using Awarean.Sdk.Result;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

public interface IAuthenticationManagerService
{
    Task<Result<AuthenticationTokens>> AuthenticateAsync(User user);
    Task<Result> ValidateTokenAsync(string tokens);
}
