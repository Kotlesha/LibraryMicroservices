using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Common.Extensions;
using User.API.Metadata.User;
using User.Application.Features.User.Commands.Create;

namespace User.API.Endpoints.User;

public static class CreateUserEndpoint
{
    public static void MapCreateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/create", 
            async (
            [FromBody] CreateUserCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(command, ct);

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
    }
}
