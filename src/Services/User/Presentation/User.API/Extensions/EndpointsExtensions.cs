using User.API.Endpoints.User;

namespace User.API.Extensions;

public static class EndpointsExtensions
{
    public static void UseEndpoints(this IEndpointRouteBuilder app)
    {
        //app.MapGroup("/users");

        app.MapCreateUserEndpoint();
        app.MapGetUserByIdEndpoint();
        app.MapGetAuthUserEndpoint();
        app.MapGetAllUsersEndpoint();
    }
}
