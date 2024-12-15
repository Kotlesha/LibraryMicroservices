using Auth.DAL.Context;
using Auth.DAL.Repositories.Implementations;
using Auth.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.DAL.Extensions;

public static class DataExtensions
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<AccountDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("AccoundDbConnectionString")));

        return services;
    }
}
