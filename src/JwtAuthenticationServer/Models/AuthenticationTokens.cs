namespace JwtAuthenticationServer.Models
{
    public class AuthenticationTokens
    {
        public string AcessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public int ExpiresIn { get; private set; }
        public IEnumerable<string> Scopes { get; set; }

        public AuthenticationTokens(string acessToken, string refreshToken, int expiresIn, IEnumerable<string> scopes = null)
        {
            AcessToken = acessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            Scopes ??= new List<string>();
        }

        public static readonly AuthenticationTokens Null = new AuthenticationTokens(string.Empty, string.Empty, -1);

    }
}