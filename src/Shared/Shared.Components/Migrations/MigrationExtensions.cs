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

        // Ищем все зарегистрированные сервисы, наследующиеся от DbContext
        var dbContextTypes = app.ApplicationServices.GetService<IServiceCollection>()
            ?.Where(descriptor => typeof(DbContext).IsAssignableFrom(descriptor.ServiceType))
            .Select(descriptor => descriptor.ServiceType)
            .Distinct();

        if (dbContextTypes == null || !dbContextTypes.Any())
        {
            throw new InvalidOperationException("No DbContext instances found in the DI container.");
        }

        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = serviceProvider.GetService(dbContextType) as DbContext;
            dbContext?.Database.Migrate();
        }
    }
}
