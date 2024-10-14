using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Context;

namespace User.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using UserDbContext context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        context.Database.Migrate();
    }
}
