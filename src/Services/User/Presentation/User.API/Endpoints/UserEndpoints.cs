using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using User.Application.Features.User.Commands.Create;
using User.Application.Features.User.Queries.GetAll;
using User.Application.Features.User.Queries.GetAuth;
using User.Application.Features.User.Queries.GetById;
using User.Application.Features.User.Queries.RequestDTOs;
using Shared.CleanArchitecture.Extensions;

namespace User.API.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("users");

        endpoints.MapPost("/create", CreateUser)
            .WithName(nameof(CreateUser));

        endpoints.MapGet("/", GetAllUsers)
            .WithName(nameof(GetAllUsers));

        endpoints.MapGet("/me", GetAuthUser)
            .WithName(nameof(GetAuthUser));

        endpoints.MapGet("{applicationUserId:guid}", GetUserById)
            .WithName(nameof(GetUserById));

        return app;
    }

    private static async Task<IResult> CreateUser(
        [FromBody] CreateUserCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Results.CreatedAtRoute(
                routeName: nameof(GetUserById),
                routeValues: new { applicationUserId = result.Value },
                result.Value);
        }

        return result.ToProblemDetails();
    }

    private static async Task<IResult> GetAllUsers(
        ISender sender,
        [AsParameters] UserParameters parameters,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var (users, metaData) = await sender.Send(
            new GetAllUsersQuery(parameters), cancellationToken);

        if (users.Any())
        {
            httpContext.Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(metaData));

            return Results.Ok(users);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> GetAuthUser(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAuthUserQuery(), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetUserById(
        Guid applicationUserId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetUserByIdQuery(applicationUserId),
            cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }
}
