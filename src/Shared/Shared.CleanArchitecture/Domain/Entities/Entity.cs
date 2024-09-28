namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class Entity<T> : IEntity<T>, IAuditableEntity
{
    public T Id { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
