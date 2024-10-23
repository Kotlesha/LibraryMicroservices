using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.CleanArchitecture.Common;
using User.Application.Features.User.Commands.Create;

namespace User.API.Endpoints.User;

public sealed class CreateUserEndpoint(ISender sender) 
    : Endpoint<CreateUserCommand, Results<CreatedAtRoute<Guid>, Conflict<Error>>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Post("/create");
        Description(e => e.WithTags("Users"));
        AllowAnonymous();
    }

    public override async Task<Results<CreatedAtRoute<Guid>, Conflict<Error>>> ExecuteAsync(
        CreateUserCommand req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        if (result.IsSuccess)
        {
            var createdResult = TypedResults.CreatedAtRoute(
                routeName: nameof(GetUserByIdEndpoint),
                routeValues: new { userId = result.Value },
                value: result.Value);

            return createdResult;
        }

        return TypedResults.Conflict(result.Error);
    }
}
