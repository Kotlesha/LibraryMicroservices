using System.Text.Json.Serialization;

namespace Shared.CleanArchitecture.Domain.Entities;

public abstract class Entity<T> : ValidatableEntity<Entity<T>, T>, IEntity<T>, IAuditableEntity
{

    public T Id { get; protected set; }

    protected Entity(T id) => Id = id;

    [JsonIgnore]
    public DateTime? CreatedOnUtc { get; set; }
    [JsonIgnore]
    public string? CreatedBy { get; set; }
    [JsonIgnore]
    public DateTime? LastModifiedOnUtc { get; set; }
    [JsonIgnore]
    public string? LastModifiedBy { get; set; }
}
