using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using User.API.Examples.User.CreateUser;
using User.Application.Errors;
using User.Application.Features.User.Commands.Create;

namespace User.API.Metadata.User;

public static class CreateUserEndpointMetadata
{
    public static RouteHandlerBuilder ApplyCreateUserMetadata(this RouteHandlerBuilder route)
    {
        route
            .WithName("CreateUser")
            .WithTags("Users")
            .WithSummary("Creates a new user")
            .WithDescription(
                "This endpoint allows for the creation of a new user by sending a CreateUserCommand.");

        route
             .WithMetadata(
                new SwaggerRequestExampleAttribute(
                    typeof(CreateUserCommand),
                    typeof(CreateUserCommandExample)));

        route
            .Produces<Guid>(StatusCodes.Status201Created)
            .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status201Created,
                    "User successfully created"));

        route
             .ProducesProblem(StatusCodes.Status409Conflict)
             .WithMetadata(
                new SwaggerResponseAttribute(
                    StatusCodes.Status409Conflict,
                    $"Conflict: {ApplicationErrors.User.ApplicationUserIdAlreadyExists.Message}"),
                new SwaggerResponseExampleAttribute(
                    StatusCodes.Status409Conflict,
                    typeof(CreateUserConflictResponseExample)));

        return route;
    }
}
