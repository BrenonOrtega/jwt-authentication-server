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
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationManagerService authenticationManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
    }

    [HttpPost]
    [Route("{Action}")]
    public async Task<IActionResult> Login(User user)
    {
        var tokensResult = await _authenticationManager.AuthenticateAsync(user);

        if(tokensResult.IsFailed)
        {
            _logger.LogWarning("Failed authenticating user {username}. Error: {error}, Message: {message}",
                user.Name, tokensResult.Error.Code, tokensResult.Error.Message);
            return Forbid();
        }

        var tokens = tokensResult.Value;
        _logger.LogInformation("Succesfully Acquired token for user {username}", user.Name);
        
        return Ok(AuthTokensResponse.FromAuthorizationTokens(tokens));
    }
}
