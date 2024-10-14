namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class Entity<T> : ValidatableEntity<Entity<T>, T>, IEntity<T>, IAuditableEntity
{
    public T Id { get; protected set; }

    protected Entity(T id) => Id = id;

    public DateTime? CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
}
