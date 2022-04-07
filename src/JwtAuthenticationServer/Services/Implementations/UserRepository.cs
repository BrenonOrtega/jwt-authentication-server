using Awarean.Sdk.Result;
using Awarean.Sdk.Utils;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;

class UserRepository : IUserRepository
{
    IDictionary<Guid, UserData> _users = new Dictionary<Guid, UserData>(Generate(5));

    public static readonly Error UserNotFound = Error.Create("DID_NOT_FOUND_USER", "Could not find user that matches supplied user.");

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

    public Task<Result<bool>> ExistsAsync(User user)
    {
        var result = _users.Any(EvaluateUser(user.Name, user.Password)) 
            ?   Result<bool>.Success(true)
            :   Result<bool>.Fail(UserNotFound);
        
        return Task.FromResult(result);
    }

    private static Func<KeyValuePair<Guid, UserData>, bool> EvaluateUser(string name, string password)
    {
        return x => x.Value.User.Name == name && x.Value.User.Password == password;
    }

    public Task<Result<UserData>> GetByIdAsync(Guid id)
    {
        var exists = _users.TryGetValue(id, out var user);

        return Task.FromResult(exists ? Result<UserData>.Success(user) : HandleNotFoundUser<UserData>());
    }


    public Task<Result<UserData>> GetUserAsync(string name, string password)
    {
        if(name.IsNullOrEmpty() || password.IsNullOrEmpty())
            return Task.FromResult(Result<UserData>.Fail("INVALID_PARAMETERS", "Invalid username or password"));
        
        var userData = _users.SingleOrDefault(EvaluateUser(name, password)).Value;

        return Task.FromResult(Result<UserData>.Success(userData));
    }

    public async Task<Result<UserData>> TryGetAsync(User user)
    {
        var found = _users.FirstOrDefault(EvaluateUser(user.Name, user.Password)).Value;

        if(found is null)
            return Result<UserData>.Fail("NOT_FOUND", "Did not found user matching supplied informations");
        
        return Result<UserData>.Success(found);
    }

    private Result<T> HandleNotFoundUser<T>() => Result<T>.Fail(UserNotFound);
}