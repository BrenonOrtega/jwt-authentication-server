using Awarean.Sdk.Result;
using Bogus;
using FluentAssertions;
using JwtAuthenticationServer.Controllers;
using JwtAuthenticationServer.Models;
using JwtAuthenticationServer.Models.Responses;
using JwtAuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace JwtAuthenticationServer.Tests.Controllers;

public class AuthenticationControllerTests
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationManagerService _authenticationManager;

    public AuthenticationControllerTests() =>
        (_logger, _authenticationManager) = (Substitute.For<ILogger<AuthenticationController>>(), Substitute.For<IAuthenticationManagerService>());

    [Fact]
    public async void Valid_User_Login_Should_Generate_Token()
    {
        var expected = new AuthenticationTokens(new string('1', 75), 
            new string('2', 33), 
            TimeSpan.FromHours(3).Seconds, 
            new List<string>(5) { "create", "read", "delete", "update" });

        var sut = new AuthenticationController(_logger, _authenticationManager);

        _authenticationManager.AuthenticateAsync(default).ReturnsForAnyArgs(Result<AuthenticationTokens>.Success(expected));

        var actionResult = await sut.Login(new User() { Name = "test user 1", Password = "test password 1" }) as OkObjectResult;
        var tokenResult = actionResult.Value as AuthTokensResponse;
        var tokens = tokenResult;

        tokens.access_token.Should().Be(expected.AcessToken);
        tokens.refresh_token.Should().Be(expected.RefreshToken);
        tokens.expires_in.Should().Be(expected.ExpiresIn);
        tokens.scopes.Should().ContainAll(expected.Scopes);
    }

    [Fact]
    public async void Invalid_User_Should_Forbid_Access()
    {
        _authenticationManager.AuthenticateAsync(default).ReturnsForAnyArgs(Result<AuthenticationTokens>.Fail("INVALID_USER", "this user does not exist"));
        var sut = new AuthenticationController(_logger, _authenticationManager);

        var result = await sut.Login(new User { Name = "any invalid name", Password = "any invalid password" });

        result.Should().BeOfType<ForbidResult>();
    }
}
