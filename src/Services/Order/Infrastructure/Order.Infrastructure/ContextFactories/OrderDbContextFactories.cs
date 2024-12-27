using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Order.Infrastructure.Contexts;

namespace Order.Infrastructure.ContextFactories;

internal class OrderDbContextFactories : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<OrderDbContext>();
        builder.UseSqlServer();
        
        return new OrderDbContext(builder.Options);
    }
}
