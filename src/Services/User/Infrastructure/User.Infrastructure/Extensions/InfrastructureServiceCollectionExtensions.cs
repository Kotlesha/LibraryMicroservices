using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CleanArchitecture.Domain.Repositories;
using User.Domain.Repositories;
using User.Infrastructure.Context;
using User.Infrastructure.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace User.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            configuration.GetConnectionString("UserDbConnectionString"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}