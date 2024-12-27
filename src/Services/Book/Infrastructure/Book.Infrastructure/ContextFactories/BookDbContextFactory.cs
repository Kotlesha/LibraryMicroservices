using Book.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Book.Infrastructure.ContextFactories;

internal class BookDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
{
    public BookDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<BookDbContext>();
        builder.UseSqlServer();

        return new BookDbContext(builder.Options);
    }
}