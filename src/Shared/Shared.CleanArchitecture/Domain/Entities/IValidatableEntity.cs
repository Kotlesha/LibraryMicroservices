namespace Shared.CleanArchitecture.Domain.Entities;

public interface IValidatableEntity<TEntity, TKey> 
    where TEntity : IEntity<TKey>
{
    void Validate();
}
