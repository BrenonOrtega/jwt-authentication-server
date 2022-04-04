namespace JwtAuthenticationServer.Models.Responses;

public class AuthTokensResponse
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public int expires_in { get; set; }
    public string scopes { get; set; }

    public static AuthTokensResponse FromAuthorizationTokens(AuthenticationTokens tokens)
    {
        return new AuthTokensResponse
        {
            access_token = tokens.AcessToken,
            refresh_token = tokens.RefreshToken,
            expires_in = tokens.ExpiresIn,
            scopes = string.Join(",", tokens.Scopes)
        };
    }
}