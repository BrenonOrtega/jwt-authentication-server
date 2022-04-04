using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

public class AuthenticationManagerService : IAuthenticationManagerService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;
    
    public AuthenticationManagerService(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }


    public Task<AuthenticationTokens> AuthenticateAsync(User user)
    {
        return Task.FromResult<AuthenticationTokens>(null);
    }
}
