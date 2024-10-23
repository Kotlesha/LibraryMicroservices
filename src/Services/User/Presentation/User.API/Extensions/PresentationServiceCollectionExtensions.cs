using FastEndpoints;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Presentation.Providers;
using FastEndpoints.Swagger;

namespace User.API.Extensions;

public static class PresentationServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdProvider, UserIdProvider>();

        services.AddFastEndpoints()
            .SwaggerDocument(o => o.AutoTagPathSegmentIndex = 0);

        return services;
    }
}
