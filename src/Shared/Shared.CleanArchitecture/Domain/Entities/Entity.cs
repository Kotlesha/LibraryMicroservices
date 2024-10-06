namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class Entity<T> : ValidatableEntity<Entity<T>, T>, IEntity<T>, IAuditableEntity
{
    public T Id { get; protected set; }

    protected Entity(T id) => Id = id;

    public DateTimeOffset? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
