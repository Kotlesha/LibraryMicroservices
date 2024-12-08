using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Common.Extensions;
using Swashbuckle.AspNetCore.Filters;
using User.API.Metadata.User;
using User.Application.Features.User.Commands.Create;
using User.Application.Features.User.Queries.GetById;

namespace User.API.Endpoints.User;

public static class CreateUserEndpoint
{
    public static IEndpointRouteBuilder MapCreateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/create",
            async (
            [FromBody] CreateUserCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Results.CreatedAtRoute(
                    routeName: "GetUserById",
                    routeValues: new { applicationUserId = result.Value },
                    result.Value);
            }

            return result.ToProblemDetails();
        })
        .ApplyCreateUserMetadata();

        return app;
    }
}
