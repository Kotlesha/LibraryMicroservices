using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstractions.Services;
using Order.Application.Consumers;
using Order.Application.Services;
using Shared.CleanArchitecture.Application.Behaviours;
using Shared.Messaging.MassTransit.Extensions;
using System.Reflection;

namespace Order.Application.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddScoped<IOrderService, OrderService>();

        services.AddMassTransitWithRabbitMq(configuration, busConfigurator =>
        {
            busConfigurator.AddConsumer<BookCreatedConsumer>();
            busConfigurator.AddConsumer<BookDeletedConsumer>();
            busConfigurator.AddConsumer<BookUpdatedConsumer>();
        });

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        ValidatorOptions.Global.LanguageManager.Enabled = false;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes : true);

        services.AddAutoMapper(assembly);

        return services;
    }
}
