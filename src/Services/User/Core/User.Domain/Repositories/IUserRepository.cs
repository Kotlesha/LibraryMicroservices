namespace User.Domain.Repositories;

using User = Entities.User;

public interface IUserRepository
{
    Task AddUserAsync(User user, 
        CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid applicationUserId,
        CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email,
        CancellationToken cancellationToken = default);
    IQueryable<User> GetAllUsers();
}
