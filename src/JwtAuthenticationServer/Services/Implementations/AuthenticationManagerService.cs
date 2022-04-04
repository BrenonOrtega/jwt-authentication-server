using Awarean.Sdk.Result;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

class AuthenticationManagerService : IAuthenticationManagerService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;
    
    public AuthenticationManagerService(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }


    public Task<Result<AuthenticationTokens>> AuthenticateAsync(User user)
    {
        return Task.FromResult(Result<AuthenticationTokens>.Success(null));
    }
}
