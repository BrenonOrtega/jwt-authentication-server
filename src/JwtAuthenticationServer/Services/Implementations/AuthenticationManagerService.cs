using Awarean.Sdk.Result;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

class AuthenticationManagerService : IAuthenticationManagerService
{
    private readonly ILogger<IAuthenticationManagerService> _logger;
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public AuthenticationManagerService(ILogger<IAuthenticationManagerService> logger, IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public async Task<Result<AuthenticationTokens>> AuthenticateAsync(User user)
    {
        var exists = await _userRepo.ExistsAsync(user);

        if(exists is false)
            return Result<AuthenticationTokens>.Fail(KnownErrors.USER_DOES_NOT_EXIST);

        return Result<AuthenticationTokens>.Success(null);
    }
}
