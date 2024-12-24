using Order.API.Endpoints;

namespace Order.API.Extensions;

public static class EndpointBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("orders-api");

        group.MapOrderEndpoints();
        group.MapBookEndpoints();

        return app;
    }
}
