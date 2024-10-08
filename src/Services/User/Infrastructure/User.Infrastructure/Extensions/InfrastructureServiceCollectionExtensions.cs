using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.Repositories;
using User.Infrastructure.Context;
using User.Infrastructure.Repositories;

namespace User.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddDbContext<UserDbContext>(options =>
            configuration.GetConnectionString("UserDbConnectionString"));

        
        return services;
    }
}