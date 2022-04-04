namespace JwtAuthenticationServer.Models
{
    public class AuthenticationTokens
    {
        public string AcessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }

        public IEnumerable<string> Scopes { get; set; } = new List<string>();
    }
}