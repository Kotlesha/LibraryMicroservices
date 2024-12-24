using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CleanArchitecture.Application.Behaviours;
using System.Reflection;
using User.Application.Abstractions.Services;
using User.Application.Services;
using Shared.Messaging.MassTransit.Extensions;
using User.Application.Consumers;

namespace User.Application.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var assesmbly = Assembly.GetExecutingAssembly();

        services.AddScoped<IUserService, UserService>();

        services.AddMassTransitWithRabbitMq(configuration, busConfigurator =>
        {
            busConfigurator.AddConsumer<AccountCreatedConsumer>();
        });

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assesmbly));

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(assesmbly, includeInternalTypes: true);
        services.AddAutoMapper(assesmbly);

        return services;
    }
}
