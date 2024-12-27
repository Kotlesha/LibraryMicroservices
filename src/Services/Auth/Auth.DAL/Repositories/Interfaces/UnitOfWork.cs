using Auth.DAL.Context;
using Auth.DAL.Repositories.Implementations;

namespace Auth.DAL.Repositories.Interfaces;

public class UnitOfWork(AccountDbContext accountDbContext) : IUnitOfWork
{
    private readonly AccountDbContext _accountDbContext = accountDbContext;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _accountDbContext.SaveChangesAsync(cancellationToken);
    }
}
