using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using User.API.Metadata.User;
using User.Application.Features.User.Queries.GetAll;
using User.Application.Features.User.Queries.RequestDTOs;

namespace User.API.Endpoints.User;

public static class GetAllUsersEndpoint
{
    public static void MapGetAllUsersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/",
            async (
                ISender sender,
                [AsParameters] UserParameters parameters,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                var (users, metaData) = await sender.Send(
                    new GetAllUsersQuery(parameters),
                cancellationToken);

                if (users.Any())
                {
                    httpContext.Response.Headers.Append(
                        "X-Pagination",
                        JsonSerializer.Serialize(metaData));

                    return Results.Ok(users);
                }

                return Results.NoContent();
            })
        .ApplyGetAllUsersMetadata();
    }
}
