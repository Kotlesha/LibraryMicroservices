using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Repositories;
using Order.Infrastructure.Contexts;
using Order.Infrastructure.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Order.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options => {
            options.UseSqlServer(
                configuration.GetConnectionString("OrderDbConnectionStringDocker"));
        });

        services.AddScoped<IUnitOfWork>(
            provider =>
            {
                var userDbContext = provider.GetRequiredService<OrderDbContext>();
                var userIdProvider = provider.GetRequiredService<IUserIdProvider>();
                return new UnitOfWork(userDbContext, userIdProvider);
            });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}
