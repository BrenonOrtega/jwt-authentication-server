namespace JwtAuthenticationServer.Models
{
    public class UserData
    {
        public Guid Id { get; internal set; }
        public bool Active { get; internal set; }
        public User User { get; internal set; }

        public IDictionary<string, string> Claims { get; init; } = new Dictionary<string, string>();

        public UserData() {  }

        public UserData(Guid id, User user, IEnumerable<KeyValuePair<string, string>> claims)
        {
            Id = id;
            User = user;
            Active = true;
            
            foreach (var claim in claims)
                Claims.Add(claim);
        }

        public readonly static UserData Null = new UserData() 
        {
            Id = Guid.Empty,
            Active = false,
            User = User.Null
        };
    }
}