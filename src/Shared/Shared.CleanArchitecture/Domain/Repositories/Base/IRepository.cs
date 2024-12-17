using Shared.CleanArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace Shared.CleanArchitecture.Domain.Repositories.Base;

public interface IRepository<T> :
    IRepository<T, Guid>
    where T : AggregateRoot<Guid>;

public interface IRepository<T, Tkey> where T : AggregateRoot<Tkey>
{
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
}
