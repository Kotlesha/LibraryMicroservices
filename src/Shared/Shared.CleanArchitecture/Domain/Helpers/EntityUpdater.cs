using Shared.CleanArchitecture.Domain.Entities;

namespace Shared.CleanArchitecture.Domain.Helpers;

public static class EntityUpdater
{
    public static void UpdateRelatedEntities<TEntity>(
        IEnumerable<TEntity> currentEntities,
        IEnumerable<TEntity> newEntities,
        Action<TEntity> addEntity,
        Action<TEntity> removeEntity)
        where TEntity : AggregateRoot
    {
        var currentEntityIds = currentEntities.Select(e => e.Id).ToHashSet();
        var newEntityIds = newEntities.Select(e => e.Id).ToHashSet();

        foreach (var entity in currentEntities)
        {
            if (!newEntityIds.Contains(entity.Id))
            {
                removeEntity(entity);
            }
        }

        foreach (var entity in newEntities)
        {
            if (!currentEntityIds.Contains(entity.Id))
            {
                addEntity(entity);
            }
        }
    }
}
