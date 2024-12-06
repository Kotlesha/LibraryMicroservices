using Swashbuckle.AspNetCore.Annotations;
using User.API.Examples.User.GetUserById;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Metadata.User;

public static class GetUserByIdEnpointMetadata
{
    public static RouteHandlerBuilder ApplyGetUserByIdMetadata(this RouteHandlerBuilder route)
    {
        route
            .WithName("GetUserById")
            .WithTags("Users")
            .WithSummary("Retrieves a user's information by their ID")
            .WithDescription(
                "This endpoint fetches detailed information about a user by their unique identifier (userId). " +
                "If the user is not found, a NotFound response is returned.");

        route
            .WithMetadata(
                new SwaggerParameterAttribute(
                    "The unique identifier of the user (GUID format)"));

        route
            .Produces<UserDTO>()
            .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "User information",
                    typeof(GetUserByIdSuccessResponseExample)));

        route
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status404NotFound,
                    $"Not found: {ApplicationErrors.User.NotFound.Message}",
                    typeof(GetUserByIdNotFoundExample)));

        return route;
    }
}
