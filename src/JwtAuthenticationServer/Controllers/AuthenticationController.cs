using JwtAuthenticationServer.Models;
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
        var tokens = await _authenticationManager.AuthenticateAsync(user);

        return Ok(new { access_token = tokens.AcessToken, refresh_token = tokens.RefreshToken, expires_in = tokens.ExpiresIn});
    }
}
