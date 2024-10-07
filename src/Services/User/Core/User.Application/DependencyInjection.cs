using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace User.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assesmbly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assesmbly));

        services.AddValidatorsFromAssembly(assesmbly);
        services.AddAutoMapper(assesmbly);

        return services;
    }
}
