using Auth.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.DAL.ContextFactories;

internal class AccountDbContextFactory : IDesignTimeDbContextFactory<AccountDbContext>
{
    public AccountDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AccountDbContext>();
        builder.UseSqlServer();

        return new(builder.Options);
    }
}
