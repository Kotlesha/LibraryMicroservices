using Shared.CleanArchitecture.Domain.Entities;

namespace Shared.CleanArchitecture.Domain.Repositories.Base;

public interface IEntityBatchRepository<T> : IEntityBatchRepository<T, Guid>
    where T : AggregateRoot<Guid>;

public interface IEntityBatchRepository<T, TKey> :
    IRepository<T, TKey>
    where T : AggregateRoot<TKey>
{
    Task<List<T>> GetExistingEntitiesByIdsAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default);
}
