using Bogus;
using JwtAuthenticationServer.Controllers;
using JwtAuthenticationServer.Models;
using JwtAuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace JwtAuthenticationServer.Tests.Controllers;

public class AuthenticationControllerTests
{

    [Fact]
    public async void Valid_User_Login_Should_Return_Token()
    {
        var faker = new Faker();
        var logger = Substitute.For<ILogger<AuthenticationController>>();
        var authenticationManager = Substitute.For<IAuthenticationManagerService>();

        var expected = new AuthenticationTokens() 
        { 
            AcessToken = new string('1', 75), 
            RefreshToken = new string('2', 33),
            ExpiresIn = System.DateTime.Now.AddHours(1)
        };
        
        var sut = new AuthenticationController(logger, authenticationManager);

        authenticationManager.AuthenticateAsync(default).ReturnsForAnyArgs(expected);

        dynamic result = await sut.Login(new User() { Name = "test user 1", Password = "test password 1"}) as OkObjectResult;

        result.Value.access_token.Should().Be(expected.AcessToken);
        result.Value.refresh_token.Should().Be(expected.RefreshToken);
        result.Value.expires_in.Should().Be(expected.ExpiresIn);      
    }
}
