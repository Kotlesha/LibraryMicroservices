using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using User.Infrastructure.Context;

namespace User.Infrastructure.ContextFactories;

internal class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
        optionsBuilder.UseSqlServer();

        return new UserDbContext(optionsBuilder.Options);
    }
}
