using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Shared.CleanArchitecture.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> 
    where T : AggregateRoot
{
    private readonly DbContext _dbContext;

    protected Repository(DbContext dbContext) => _dbContext = dbContext;

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext
            .Set<T>()
            .AddAsync(
                entity, 
                cancellationToken);
    }

    public Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext
            .Set<T>()
            .Remove(entity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext
            .Set<T>()
            .Update(entity);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<T>()
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(
                ag => ag.Id.Equals(Id),
                cancellationToken);
    }

    public async Task<IEnumerable<T>> GetByPredicateAsync(
        Expression<Func<T, bool>> predicate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<T>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }
}
