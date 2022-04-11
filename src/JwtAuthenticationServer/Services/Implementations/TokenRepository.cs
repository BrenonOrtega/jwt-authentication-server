using System.Text.Json;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

class TokenRepository : ITokenRepository
{
    const string FILE_PATH = @"..\tokens.json";
    
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

    public async Task SaveToken(AuthenticationTokens tokens)
    {
        var exists = File.Exists(FILE_PATH);

        if(exists is false)
        {
            using var stream = File.Create(FILE_PATH);
            JsonSerializer.Serialize(stream, new object[0]);
        }

        var file = await File.ReadAllTextAsync(FILE_PATH);

        var allTokens = JsonSerializer.Deserialize<List<AuthenticationTokens>>(file);

        allTokens.Add(tokens);

        File.WriteAllText(FILE_PATH, JsonSerializer.Serialize(allTokens));
    }
}