using JwtAuthenticationServer.Models;
using JwtAuthenticationServer.Models.Responses;
using JwtAuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationServer.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationManagerService _authenticationManager;
    public ILogger<AuthenticationController> Logger { get; }

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationManagerService authenticationManager)
    {
        this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        var tokensResult = await _authenticationManager.AuthenticateAsync(user);

        if(tokensResult.IsFailed)
            return Forbid();

        var tokens = tokensResult.Value;
        return Ok(AuthTokensResponse.FromAuthorizationTokens(tokens));
    }
}
