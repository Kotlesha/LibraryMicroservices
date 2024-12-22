using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Services;
using Shared.CleanArchitecture.Application.Behaviours;
using System.Reflection;

namespace Order.Application.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddScoped<IOrderService, OrderService>();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes : true);

        services.AddAutoMapper(assembly);

        return services;
    }
}
