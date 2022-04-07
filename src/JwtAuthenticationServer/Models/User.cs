namespace JwtAuthenticationServer.Models;

public class User
{
    public static readonly User Null = new User();

    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
}
