namespace Shared.CleanArchitecture.Domain.Entities;

public interface IEntity<T>
{
    T Id { get; }
}
