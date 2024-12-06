using Microsoft.EntityFrameworkCore;
using User.Domain.Repositories;
using User.Infrastructure.Context;

namespace User.Infrastructure.Repositories;

using User = Domain.Entities.User;

internal class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    private readonly UserDbContext _userDbContext = userDbContext;

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _userDbContext.AddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .FirstOrDefaultAsync(
                u => u.Email.Equals(email),
                cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid applicationUserId, CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.ApplicationUserId.Equals(applicationUserId), 
                cancellationToken);
    }
}
