using Auth.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.PL.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AccountDbContext accountDbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();

        accountDbContext.Database.Migrate();
    }
}
