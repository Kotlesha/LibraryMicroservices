using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging.MassTransit.Extensions;

public static class MassTransitExtensions
{
    public static void AddMassTransitWithRabbitMq(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddMassTransitWithRabbitMq(configuration, null);
    }

    public static void AddMassTransitWithRabbitMq(this IServiceCollection services, 
        IConfiguration configuration, 
        Action<IBusRegistrationConfigurator>? configureConsumers)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            configureConsumers?.Invoke(busConfigurator);

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                {
                    h.Username(configuration["MessageBroker:Username"]!);
                    h.Password(configuration["MessageBroker:Password"]!);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }
}


