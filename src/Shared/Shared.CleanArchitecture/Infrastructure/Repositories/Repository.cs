using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Shared.CleanArchitecture.Infrastructure.Repositories;

public abstract class Repository<T>(DbContext dbContext) : IRepository<T> 
    where T : AggregateRoot
{
    private readonly DbContext _dbContext = dbContext;

    public IQueryable<T> GetAll() => _dbContext.Set<T>().AsNoTracking();

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
    {
        return _dbContext.Set<T>().Where(expression).AsNoTracking();
    }

    public void Add(T entity) => _dbContext.Set<T>().Add(entity);

    public void Update(T entity) => _dbContext.Set<T>().Update(entity);

    public void Remove(T entity) => _dbContext.Set<T>().Remove(entity);

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await GetByCondition(ar => ar.Id.Equals(id))
            .FirstOrDefaultAsync();
    }
}
