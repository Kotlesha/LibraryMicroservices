using Shared.CleanArchitecture.Domain.Repositories;

namespace User.Domain.Repositories;

using User = Entities.User;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetAuthUser(Guid userId);
}
