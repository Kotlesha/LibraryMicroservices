using Shared.CleanArchitecture.Domain.Entities;

namespace Shared.CleanArchitecture.Domain.Helpers;

public static class EntityUpdater
{
    public static void UpdateRelatedEntities<TEntity>(
        IEnumerable<TEntity> currentEntities,
        IEnumerable<TEntity> newEntities,
        Action<TEntity> addEntity,
        Action<TEntity> removeEntity) where TEntity : AggregateRoot
    {
        var currentEntityIds = currentEntities.Select(e => e.Id).ToHashSet();
        var newEntityIds = newEntities.Select(e => e.Id).ToHashSet();

        var entitiesToRemove = currentEntities.Where(e => !newEntityIds.Contains(e.Id)).ToList();
        var entitiesToAdd = newEntities.Where(e => !currentEntityIds.Contains(e.Id)).ToList();

        foreach (var entity in entitiesToRemove)
        {
            removeEntity(entity);
        }

        foreach (var entity in entitiesToAdd)
        {
            addEntity(entity);
        }
    }

}
