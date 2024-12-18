using Auth.PL.Endpoints;

namespace Auth.PL.Extensions;

public static class EndpointBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth-api");

        group.MapAccountEndpoints();
        group.MapRefreshTokenEndpoints();

        return group;
    }
}
