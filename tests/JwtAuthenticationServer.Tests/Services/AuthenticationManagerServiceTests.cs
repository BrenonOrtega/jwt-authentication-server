using JwtAuthenticationServer.Services;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Xunit;
using JwtAuthenticationServer.Models;
using FluentAssertions;

namespace JwtAuthenticationServer.Tests.Services;
public class AuthenticationManagerServiceTests
{
    private readonly ILogger<IAuthenticationManagerService> _logger;
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public AuthenticationManagerServiceTests() => (_logger, _userRepo, _tokenService) =
            (Substitute.For<ILogger<IAuthenticationManagerService>>(), Substitute.For<IUserRepository>(), Substitute.For<ITokenService>());

    [Fact]
    public async void Non_Existing_User_Should_Not_Authenticate()
    {
        _userRepo.GetUserAsync(default, default).ReturnsForAnyArgs<UserData>(null, new UserData[] { null });

        var sut = new AuthenticationManagerService(_logger, _userRepo, _tokenService);

        var result = await sut.AuthenticateAsync(new User { Name = "Any", Password = "any password" });

        result.IsFailed.Should().BeTrue();
        result.Error.Should().Be(KnownErrors.USER_DOES_NOT_EXIST);
    }

    [Fact]
    public async void Existing_User_Should_Authenticate()
    {
        _userRepo.GetUserAsync(default, default)
            .ReturnsForAnyArgs(new UserData { Active = true, Id = Guid.NewGuid(), User = new User { Name = "Test User", Password = "Test Password" } });

        _tokenService.GenerateTokenAsync(default).ReturnsForAnyArgs(new AuthenticationTokens(new string('1', 240), new string('1', 90), 2400, new string[10]));

    }

}