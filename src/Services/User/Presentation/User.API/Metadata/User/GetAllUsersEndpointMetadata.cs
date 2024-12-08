using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
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
                "This endpoint retrieves a list of users with pagination, " +
                "including metadata about the pagination state (e.g., total pages, current page).");

        route
            .Produces<IEnumerable<UserDTO>>()
            .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "List of users"),
                new SwaggerResponseExampleAttribute(
                    StatusCodes.Status200OK,
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
