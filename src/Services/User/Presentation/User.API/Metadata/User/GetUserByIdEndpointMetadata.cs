using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using User.API.Examples.Common;
using User.API.Examples.User.GetUserById;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Metadata.User;

public static class GetUserByIdEndpointMetadata
{
    public static RouteHandlerBuilder ApplyGetUserByIdMetadata(this RouteHandlerBuilder route)
    {
        route
            .WithName("GetUserById")
            .WithTags("Users")
            .WithSummary("Retrieves a user's information by their Id")
            .WithDescription(
                "This endpoint fetches detailed information about a user by their unique identifier (userId). " +
                "If the user is not found, a NotFound response is returned.");

        route
            .Produces<UserDTO>()
            .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "User information",
                    typeof(GetUserSuccessResponseExample)));

        route
             .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
             .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status404NotFound,
                    $"Not found: {ApplicationErrors.User.NotFound.Message}"),
                new SwaggerResponseExampleAttribute(
                    StatusCodes.Status404NotFound,
                    typeof(GetUserByIdNotFoundExample)));
                    
        return route;
    }
}
