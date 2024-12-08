using Swashbuckle.AspNetCore.Annotations;
using User.API.Examples.User.GetAllUsers;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Metadata.User;

public static class GetAllUsersEndpointMetadata
{
    public static RouteHandlerBuilder ApplyGetAllUsersMetadata(this RouteHandlerBuilder route)
    {
        route
            .WithName("GetAllUsers")
            .WithTags("Users")
            .WithSummary("Retrieves a list of all users")
            .WithDescription(
                "This endpoint returns a list of all registered users. " +
                "If no users are found, it will return a NoContent response.");

        route
            .Produces<IEnumerable<UserDTO>>()
            .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "List of Users",
                    typeof(GetAllUsersSuccessResponseExample)));

        route
             .Produces(StatusCodes.Status204NoContent)
             .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status204NoContent,
                    "No users found"));

        return route;
    }
}
