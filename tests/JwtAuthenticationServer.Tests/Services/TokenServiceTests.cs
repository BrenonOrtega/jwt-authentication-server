using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using JwtAuthenticationServer.Models;
using JwtAuthenticationServer.Services;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace JwtAuthenticationServer.Tests.Services;

public class TokenServiceTests
{
    private readonly IOptionsMonitor<AuthOptions> _options;
    public TokenServiceTests()
    {
        _options = Substitute.For<IOptionsMonitor<AuthOptions>>();
        _options.CurrentValue.ReturnsForAnyArgs(Options);
    }

    [Fact]
    public void Should_Generate_Token_Succesfully()
    {
        // Given
        var user = ValidUser();
        var sut = new TokenService(_options);

        // When
        var token = sut.GenerateToken(user);

        // Then
        var handler = new JwtSecurityTokenHandler();

        handler.CanReadToken(token).Should().BeTrue();
    }

    private UserData ValidUser() => new UserData
    {
        Active = true,
        Id = Guid.NewGuid(),
        User = new User { Name = "Test User" },
        Claims = new Dictionary<string, string> { { "role", "admin" }, { "scopes", "create, read, update, delete" } }
    };


    public AuthOptions Options 
    {   get 
        {
            using RSA rsa = RSA.Create();

            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            var authOptions = new AuthOptions()
            {
                Audience = "my test audience",
                Authority = "my test authority",
                PrivateKey = privateKey,
                PublicKey = publicKey
            };

            return authOptions;
        } 
    }
}