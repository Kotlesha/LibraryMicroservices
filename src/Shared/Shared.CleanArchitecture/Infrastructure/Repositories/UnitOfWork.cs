using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Services;

namespace Shared.CleanArchitecture.Infrastructure.Repositories;

internal sealed class UnitOfWork(
    DbContext dbContext, 
    IUserService userService) : IUnitOfWork
{
    private readonly DbContext _dbContext = dbContext;
    private readonly IUserService _userService = userService;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOnUtc)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.CreatedBy)
                    .CurrentValue = _userService.GetAuthUserId();
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.LastModifiedOnUtc)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.LastModifiedBy)
                    .CurrentValue = _userService.GetAuthUserId();
            }
        }
    }
}
