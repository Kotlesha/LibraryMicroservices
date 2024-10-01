namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class Entity<T> : IEntity<T>, IAuditableEntity, IValidatableEntity<Entity<T>, T>
{
    public T Id { get; protected set; }

    protected Entity(T id) => Id = id;

    public DateTimeOffset? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }

    public abstract void Validate();
}
