using System.Collections.Concurrent;
using TwitterClone.Api.Dtos;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Api.Data;

public sealed class UserRepository
{
    private readonly ConcurrentDictionary<Guid, UserDto> _users = new();
    public UserRepository() { var user = new User("Shuvo", "shuvo@example.com"); _users[user.Id] = ToDto(user); }
    public IReadOnlyCollection<UserDto> GetUsers() => _users.Values.OrderBy(user => user.Username).ToArray();
    public UserDto? GetUserById(Guid id) => _users.GetValueOrDefault(id);
    public UserDto? GetUserByEmail(string email) => _users.Values.FirstOrDefault(user => user.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
    public UserDto AddUser(CreateUserRequest request) { var user = new User(request.Username, request.Email); var dto = ToDto(user); _users[dto.Id] = dto; return dto; }
    public UserDto? UpdateUser(Guid id, UpdateUserRequest request) { if (!_users.ContainsKey(id)) return null; var updated = new UserDto(id, request.Username.Trim(), request.Email.Trim()); _users[id] = updated; return updated; }
    public bool DeleteUser(Guid id) => _users.TryRemove(id, out _);
    private static UserDto ToDto(User user) => new(user.Id, user.Username, user.Email);
}
