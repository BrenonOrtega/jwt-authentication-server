using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

class TokenRepository : ITokenRepository
{
    private readonly IUserRepository userRepo;

    public TokenRepository(IUserRepository userRepo)
    {
        this.userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
    }

    public Task ExpireToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRefreshToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsActiveAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task RefreshToken(string token)
    {
        throw new NotImplementedException();
    }

    public Task SaveToken(AuthenticationTokens tokens)
    {
        throw new NotImplementedException();
    }
}