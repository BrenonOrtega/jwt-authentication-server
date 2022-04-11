using System.Net;
using JwtAuthenticationServer.Services;

namespace JwtAuthenticationServer.Attributes;

public class AuthorizeMiddleware : IMiddleware
{
    private readonly ILogger<AuthorizeMiddleware> _logger;
    private readonly IAuthenticationManagerService _manager;

    public AuthorizeMiddleware(ILogger<AuthorizeMiddleware> logger, IAuthenticationManagerService manager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var skip = context.Request.Path.Value.ToUpper().Contains("LOGIN");
        
        if(skip is false)
        {
            var authService = context.RequestServices.GetService<IAuthenticationManagerService>();
            var existsAuthorization = context.Request.Headers.TryGetValue("Authorization", out var accessToken);

            if (existsAuthorization is false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Missing Authorization.");
                return;
            }

            var token = accessToken.FirstOrDefault();
            var authorize = await _manager.ValidateTokenAsync(token);

            if (authorize.IsFailed)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Invalid Authorization token.");
                return;
            }
        }

        await next(context);
    }
}
