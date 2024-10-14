using Shared.CleanArchitecture.Domain.Events;

namespace Shared.CleanArchitecture.Domain.Entities;

public interface IAggregateRoot<T> : IEntity<T>, IAggregateRoot;

public interface IAggregateRoot : IAuditableEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
