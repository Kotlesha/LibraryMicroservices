using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using User.Application.Features.User.Queries.GetAll;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Endpoints.User;

public sealed class GetAllUsersEndpoint(ISender sender) 
    : EndpointWithoutRequest<Results<Ok<IEnumerable<UserDTO>>, NoContent>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("/");
        Description(e => e.WithTags("Users"));
        AllowAnonymous();
    }

    public override async Task<Results<Ok<IEnumerable<UserDTO>>, NoContent>> ExecuteAsync(CancellationToken ct)
    {
        var users = await _sender.Send(new GetAllUsersQuery(), ct);

        return users.Any() ?
            TypedResults.Ok(users) :
            TypedResults.NoContent();
    }
}
