namespace User.Domain.Repositories;

using User = Entities.User;

public interface IUserRepository
{
    void AddUser(User user);
    Task<User?> GetUserByIdAsync(Guid accountId,
        CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email,
        CancellationToken cancellationToken = default);
    IQueryable<User> GetAllUsers();
}
