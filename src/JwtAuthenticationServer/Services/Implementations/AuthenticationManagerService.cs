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
        var result = await _userRepo.TryGetAsync(user);

        if(result.IsFailed)
            return Result<AuthenticationTokens>.Fail(KnownErrors.USER_DOES_NOT_EXIST);

        var tokens = await _tokenService.GenerateAsync(result.Value);

        return Result<AuthenticationTokens>.Success(tokens);
    }

    public Task<Result> ValidateTokenAsync(string token)
    {
        return _tokenService.ValidateAsync(token);
    }
}
