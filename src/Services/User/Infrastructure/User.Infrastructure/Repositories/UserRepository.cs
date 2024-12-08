using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Common.Extensions;
using Shared.CleanArchitecture.Common.Paging;
using User.Domain.Parameters;
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

    public async Task<PagedList<User>> GetAllUsersAsync(UserParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var query = _userDbContext.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
        {
            query = query.Where(u => u.Name.ToLower().Equals(parameters.SearchTerm.ToLower())
                || u.Email.Equals(parameters.SearchTerm.ToLower()));
        }

        return await query
            .ToPagedList(
                parameters.PageNumber, 
                parameters.PageSize, 
                cancellationToken);
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
