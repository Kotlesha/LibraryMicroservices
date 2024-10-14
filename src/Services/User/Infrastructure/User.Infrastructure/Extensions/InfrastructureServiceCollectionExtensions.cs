using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;
using User.Domain.Repositories;
using User.Infrastructure.Context;
using User.Infrastructure.Repositories;

namespace User.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("UserDbConnectionString")));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(
            provider =>
            {
                var userDbContext = provider.GetRequiredService<UserDbContext>();
                var userIdProvider = provider.GetRequiredService<IUserIdProvider>();
                return new UnitOfWork(userDbContext, userIdProvider);
            });

        return services;
    }
}