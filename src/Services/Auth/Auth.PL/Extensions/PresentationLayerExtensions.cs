using Shared.CleanArchitecture.Presentation.Extensions;

namespace Auth.PL.Extensions;

public static class PresentationLayerExtensions
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddExtendedProblemDetails();

        return services;
    }
}
