using Book.Domain.Repositories;
using Book.Infrastructure.Contexts;
using Book.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;
using Book.Infrastructure.Repositories.Cashing;

namespace Book.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddDbContext<BookDbContext>(options => {
            options.UseSqlServer(
                configuration.GetConnectionString("BookDbConnectionStringDocker"));
        });

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });


        services.AddScoped<IUnitOfWork>(
            provider =>
            {
                var userDbContext = provider.GetRequiredService<BookDbContext>();
                var userIdProvider = provider.GetRequiredService<IUserIdProvider>();
                return new UnitOfWork(userDbContext, userIdProvider);
            });

        services.AddScoped<BookRepository>();

        services.AddScoped<IBookRepository>(sp =>
        {
            var dbContext = sp.GetRequiredService<BookDbContext>();
            var bookRepository = sp.GetRequiredService<BookRepository>();
            var cache = sp.GetRequiredService<IDistributedCache>();

            return new CashingBookRepository(dbContext, bookRepository, cache);
        });

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        return services;
    }
}