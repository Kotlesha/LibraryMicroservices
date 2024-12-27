namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class ValidatableEntity<TEntity, TKey>
    where TEntity : IEntity<TKey>
{
    protected abstract void Validate();
}
