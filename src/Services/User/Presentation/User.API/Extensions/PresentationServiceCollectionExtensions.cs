using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Presentation.Providers;
using Shared.Components.ProblemDetailsUtilities.Extensions;
using Shared.Components.Swagger;

namespace User.API.Extensions;

public static class PresentationServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdProvider, UserIdProvider>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddExtendedProblemDetails();

        return services;
    }
}
