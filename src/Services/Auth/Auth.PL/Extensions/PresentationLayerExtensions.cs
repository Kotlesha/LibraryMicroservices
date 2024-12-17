using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Presentation.Providers;
using Shared.Components.ProblemDetailsUtilities.Extensions;
using Shared.Components.Swagger;

namespace Auth.PL.Extensions;

public static class PresentationLayerExtensions
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenWithAuth();
        services.AddExtendedProblemDetails();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdProvider, UserIdProvider>();

        return services;
    }
}
