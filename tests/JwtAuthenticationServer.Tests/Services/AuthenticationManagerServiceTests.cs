using JwtAuthenticationServer.Services;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Xunit;
using JwtAuthenticationServer.Models;
using FluentAssertions;
using Awarean.Sdk.Result;

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
        var userData = new UserData 
        {   Id = Guid.NewGuid(), 
            Active = true, 
            Claims = new Dictionary<string, string>{ {"role", "admin, user"}, {"scope", "create, read, delete"}}, 
            User = new User {Name = "Test User", Password = ""}
        };

        var expected = new AuthenticationTokens(new string('1', 240), new string('1', 90), 2400, new string[10]);

        _userRepo.ExistsAsync(default).ReturnsForAnyArgs(true);
        _tokenService.GenerateAsync(default).ReturnsForAnyArgs(expected);

        var sut = new AuthenticationManagerService(_logger, _userRepo, _tokenService);

        var result = await sut.AuthenticateAsync(userData.User);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }
}