using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using System.Reflection;

namespace Order.Infrastructure.Contexts;

using Order = Domain.Entities.Order;

internal class OrderDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
