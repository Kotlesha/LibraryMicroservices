using Microsoft.EntityFrameworkCore;
using User.Domain.Repositories;
using User.Infrastructure.Context;

namespace User.Infrastructure.Repositories;

using User = Domain.Entities.User;

internal class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    private readonly UserDbContext _userDbContext = userDbContext;

    public void AddUser(User user) => _userDbContext.Users.Add(user);

    public IQueryable<User> GetAllUsers() => _userDbContext.Users;

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .FirstOrDefaultAsync(
                u => u.Email.Equals(email),
                cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.AccountId.Equals(accountId), 
                cancellationToken);
    }
}
