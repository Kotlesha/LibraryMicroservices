namespace User.Domain.Repositories;

using Shared.CleanArchitecture.Common.Paging;
using User.Domain.Parameters;
using User = Entities.User;

public interface IUserRepository
{
    Task AddUserAsync(User user, 
        CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid applicationUserId,
        CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email,
        CancellationToken cancellationToken = default);
    Task<PagedList<User>> GetAllUsersAsync(
        UserParameters parameters,
        CancellationToken cancellationToken = default);
}
