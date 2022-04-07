using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Awarean.Sdk.Result;
using JwtAuthenticationServer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationServer.Services;

class TokenService : ITokenService
{
    private readonly AuthOptions _authOptions;
    public TokenService(IOptionsMonitor<AuthOptions> authOptions)
    {
        _authOptions = authOptions?.CurrentValue ?? throw new ArgumentNullException(nameof(authOptions));
    }

    public Task<AuthenticationTokens> GenerateAsync(UserData user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateRefreshToken(UserData user)
    {
        var @bytes = new byte[64];
        return Task.FromResult("");
    }

    public Task<string> GenerateRefreshTokenAsync(UserData user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateTokenAsync(UserData user)
    {
        using var encryption = RSA.Create();

        encryption.ImportRSAPrivateKey(Convert.FromBase64String(_authOptions.PrivateKey), out var _);

        var signingCredentials = new SigningCredentials(new RsaSecurityKey(encryption), SecurityAlgorithms.RsaSha256);

        var jwt = new JwtSecurityToken(
            audience: _authOptions.Audience,
            issuer: _authOptions.Authority,
            claims: user.Claims.Select(claim => new Claim(claim.Key, claim.Value)),
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Task.FromResult(token);
    }

    public Task<Result> ValidateAsync(string token)
    {
        throw new NotImplementedException();
    }
}
