using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Components.Migrations;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app) 
        //where TContext : DbContext
    {
        //using var scope = app.ApplicationServices.CreateScope();
        //var context = scope.ServiceProvider.GetRequiredService<TContext>();
        //context.Database.Migrate();

        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var dbContexts = serviceProvider.GetServices<DbContext>();

        foreach (var context in dbContexts)
        {
            context.Database.Migrate();
        }
    }
}
