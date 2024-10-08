using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace User.Infrastructure.Context;

using User = Domain.Entities.User;

internal class UserDbContext(DbContextOptions<UserDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
