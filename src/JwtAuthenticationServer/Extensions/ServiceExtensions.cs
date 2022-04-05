using JwtAuthenticationServer.Models;
using JwtAuthenticationServer.Services;

namespace JwtAuthenticationServer.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddDefaultTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationManagement()
            .AddUserRepository()
            .AddTokenRepository()
            .AddTokenGeneration(configuration);

        return services;
    }

    public static IServiceCollection AddTokenGeneration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }

    public static IServiceCollection AddUserRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddTokenRepository(this IServiceCollection services)
    {
        services.AddScoped<ITokenRepository, TokenRepository>();
        return services;
    }

    public static IServiceCollection AddAuthenticationManagement(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationManagerService, AuthenticationManagerService>();
        return services;
    }

}