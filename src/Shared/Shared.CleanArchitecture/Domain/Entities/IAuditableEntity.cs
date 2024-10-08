﻿namespace Shared.CleanArchitecture.Domain.Entities;

public interface IAuditableEntity
{
    DateTime? CreatedAtUtc { get; set; }
    string? CreatedBy { get; set; }
    DateTime? LastModifiedAtUtc { get; set; }
    string? LastModifiedBy { get; set; }
}
