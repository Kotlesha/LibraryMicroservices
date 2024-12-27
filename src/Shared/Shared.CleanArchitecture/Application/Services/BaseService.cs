using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Application.Abstractions.Services;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;
using Shared.Components.Results;

namespace Shared.CleanArchitecture.Application.Services;

public abstract class BaseService<TEntity, TEntityToAdd>(
    IEntityBatchRepository<TEntity> repository,
    IErrorProvider<TEntity> errorFormatter) : IBaseService<TEntity, TEntityToAdd>
    where TEntity : AggregateRoot
    where TEntityToAdd : AggregateRoot
{
    private readonly IEntityBatchRepository<TEntity> _repository = repository;
    private readonly IErrorProvider<TEntity> _errorFormatter = errorFormatter;

    public async Task<Result<IEnumerable<TEntity>>> GetEntitiesIfValidAsync(
        IEnumerable<Guid> entityIds, 
        CancellationToken cancellationToken = default)
    {
        var uniqueEntityIds = entityIds
            .Distinct()
            .ToList();

        var entities = await _repository.GetExistingEntitiesByIdsAsync(
            uniqueEntityIds, cancellationToken);

        if (entities.Count != uniqueEntityIds.Count)
        {
            return Result.Failure<IEnumerable<TEntity>>(_errorFormatter.GetError());
        }

        return entities;
    }

    public async Task<Result> ValidateAndAddEntitiesAsync(
        IEnumerable<Guid> entityIds, 
        TEntityToAdd targetEntity, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await GetEntitiesIfValidAsync(entityIds, cancellationToken);

        if (validationResult.IsFailure)
        {
            return Result.Failure(validationResult.Error);
        }

        foreach (var entity in validationResult.Value)
        {
            AddEntityToTarget(targetEntity, entity);
        }

        return Result.Success();
    }

    protected abstract void AddEntityToTarget(
        TEntityToAdd targetEntity, TEntity entity);
}
