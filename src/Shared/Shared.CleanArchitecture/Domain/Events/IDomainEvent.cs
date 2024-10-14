using MediatR;

namespace Shared.CleanArchitecture.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
