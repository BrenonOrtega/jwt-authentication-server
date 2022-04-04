namespace JwtAuthenticationServer.Models;

public class AuthOptions
{
    public string Authority { get; init; }
    public string Audience { get; init; }
    public string PrivateKey { get; init; }
    public string PublicKey { get; init; }
}