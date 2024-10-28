using Shared.CleanArchitecture.Common.Components;
using Shared.CleanArchitecture.Domain.Entities;

namespace Shared.CleanArchitecture.Application.Abstractions.Services;

public interface IBaseService<TEntity, TEntityToAdd>
    where TEntity : AggregateRoot
    where TEntityToAdd : AggregateRoot
{
    Task<Result<IEnumerable<TEntity>>> GetEntitiesIfValidAsync(
        IEnumerable<Guid> entityIds,
        CancellationToken cancellationToken = default);

    Task<Result> ValidateAndAddEntitiesAsync(
        IEnumerable<Guid> entityIds,
        TEntityToAdd targetEntity,
        CancellationToken cancellationToken = default);
}
