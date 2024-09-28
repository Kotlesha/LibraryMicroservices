using Shared.CleanArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace Shared.CleanArchitecture.Domain.Repositories;

public interface IRepository<T, Tkey> where T : AggregateRoot<Tkey>
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(Tkey Id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default); 
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
}
