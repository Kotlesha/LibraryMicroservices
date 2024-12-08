using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using User.API.Examples.Common;
using User.API.Examples.User.GetAuthUser;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Metadata.User;

public static class GetAuthUserEndpointMetadata
{
    public static RouteHandlerBuilder ApplyGetAuthUserMetadata(this RouteHandlerBuilder route)
    {
        route
            .WithName("GetAuthUser")
            .WithTags("Users")
            .WithSummary("Gets the authenticated user's information")
            .WithDescription(
                "This endpoint retrieves information about the currently authenticated user. " +
                "If the request is invalid, a BadRequest response is returned.");

        route
            .Produces<UserDTO>()
            .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "User information",
                    typeof(GetUserSuccessResponseExample)));

        route
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status400BadRequest,
                    $"Bad Request: {ApplicationErrors.User.InvalidUserIdFromat.Message}"),
                new SwaggerResponseExampleAttribute(
                    StatusCodes.Status400BadRequest,
                    typeof(GetAuthUserBadRequestResponseExample)));

        return route;
    }
}
