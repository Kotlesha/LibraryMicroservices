using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Providers;

namespace Shared.CleanArchitecture.Infrastructure.Repositories;

public class UnitOfWork(
    DbContext dbContext, 
    IUserIdProvider userIdProvider) : IUnitOfWork
{
    private readonly DbContext _dbContext = dbContext;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;

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
                    .CurrentValue = _userIdProvider.GetAuthUserId()?.ToString();
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.LastModifiedOnUtc)
                    .CurrentValue = DateTime.UtcNow;

                entityEntry.Property(a => a.LastModifiedBy)
                    .CurrentValue = _userIdProvider.GetAuthUserId()?.ToString();
            }
        }
    }
}
