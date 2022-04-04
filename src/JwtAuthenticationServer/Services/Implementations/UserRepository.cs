using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

class UserRepository : IUserRepository
{
    IDictionary<Guid, UserData> _users = new Dictionary<Guid, UserData>(Generate(5));

    private static IEnumerable<KeyValuePair<Guid, UserData>> Generate(int count)
    {
        for (var times = 0; times <= count; times++)
        {
            var id = Guid.NewGuid();
            var userData = new UserData { Id = id, Active = true, User = new User { Name = $"User{times}", Password = $"Password{times}" } };
            yield return KeyValuePair.Create<Guid, UserData>(id, userData);
        }
    }

    public Task AddAsync(User user, IEnumerable<KeyValuePair<string, string>> claims, Guid? id = default)
    {
        if(new Guid?[] { null, Guid.Empty }.Contains(id))
            id = Guid.NewGuid();

        var data = new UserData((Guid)id, user, claims);
        _users.Add(data.Id, data);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(User user)
    {
        return Task.FromResult(_users.Any(EvaluateUser(user.Name, user.Password)));
    }

    private static Func<KeyValuePair<Guid, UserData>, bool> EvaluateUser(string name, string password)
    {
        return x => x.Value.User.Name == name && x.Value.User.Password == password;
    }

    public Task<UserData> GetByIdAsync(Guid id)
    {
        _users.TryGetValue(id, out var user);

        return Task.FromResult(user);
    }

    public Task<UserData> GetUserAsync(string name, string password)
    {
        return Task.FromResult(_users.SingleOrDefault(EvaluateUser(name, password)).Value);
    }
}