using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.CleanArchitecture.Application.Behaviours;
using System.Reflection;

namespace User.Application.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assesmbly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assesmbly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(assesmbly);
        services.AddAutoMapper(assesmbly);

        return services;
    }
}
