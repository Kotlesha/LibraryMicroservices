using User.API.Endpoints;

namespace User.API.Extensions;

public static class EndpointBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users-api");

        group.MapUserEndpoints();

        return group;
    }
}
