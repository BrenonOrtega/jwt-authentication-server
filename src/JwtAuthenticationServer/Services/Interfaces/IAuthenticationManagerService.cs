using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

public interface IAuthenticationManagerService
{
    Task<AuthenticationTokens> AuthenticateAsync(User user);         
}
