namespace Shared.CleanArchitecture.Domain.Entities;

public interface IAuditableEntity
{
    DateTime? CreatedOnUtc { get; set; }
    string? CreatedBy { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
    string? LastModifiedBy { get; set; }
}
