namespace User.Domain.Repositories;

using User = Entities.User;

public interface IUserRepository
{
    Task AddUserAsync(User user, 
        CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid applicationUserId,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllUsersAsync(
        CancellationToken cancellationToken = default);
}
