using Shared.CleanArchitecture.Domain.Events;
using System.Text.Json.Serialization;

namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class AggregateRoot : AggregateRoot<Guid>
{
    protected AggregateRoot(Guid id) : base(id) { }
}

public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    [JsonIgnore]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(T id) : base(id) { }

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();

    protected bool HasEntity<TEntity>(TEntity entity, List<TEntity> entities) where TEntity : AggregateRoot<T>
        => entities.Any(e => e.Id!.Equals(entity.Id));

    protected void AddEntity<TEntity>(TEntity entity, List<TEntity> entities) where TEntity : AggregateRoot<T>
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (HasEntity(entity, entities)) return;
        entities.Add(entity);
    }

    protected void RemoveEntity<TEntity>(TEntity entity, List<TEntity> entities) where TEntity : AggregateRoot<T>
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (!HasEntity(entity, entities)) return;
        entities.Remove(entity);
    }
}
