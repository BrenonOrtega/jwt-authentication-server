using Awarean.Sdk.Result;
using JwtAuthenticationServer.Models;

namespace JwtAuthenticationServer.Services;
public interface IUserRepository
{
    Task<UserData> GetByIdAsync(Guid id);
    Task<UserData> GetUserAsync(string name, string password);
    Task<bool> ExistsAsync(User user);
    Task<Result<UserData>> TryGetAsync(User user);
    Task AddAsync(User user, IEnumerable<KeyValuePair<string, string>> claims,  Guid? id = default);
}
