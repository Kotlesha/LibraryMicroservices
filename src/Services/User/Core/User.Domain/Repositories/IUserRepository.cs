namespace User.Domain.Repositories;

using User = Entities.User;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user, 
        CancellationToken cancellationToken = default);
    Task<User> GetUserByIdAsync(Guid applicationUserId,
        CancellationToken cancellationToken = default);
    Task<User> GetAuthUserAsync(Guid applicationUserId,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllUsers(
        CancellationToken cancellationToken = default);
}
