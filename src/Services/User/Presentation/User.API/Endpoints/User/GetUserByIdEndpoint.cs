using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.CleanArchitecture.Common;
using User.Application.Features.User.Queries.GetById;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Endpoints.User;

public sealed class GetUserByIdEndpoint(ISender sender) 
    : EndpointWithoutRequest<Results<Ok<UserDTO>, NotFound<Error>>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("/{userId:guid}");

        Description(e => {
            e.WithTags("Users");
            e.WithName("GetUserByIdEndpoint");
        });

        AllowAnonymous();
    }

    public override async Task<Results<Ok<UserDTO>, NotFound<Error>>> ExecuteAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("userId");
        var result = await _sender.Send(new GetUserByIdQuery(userId), ct);

        return result.IsSuccess ?
            TypedResults.Ok(result.Value) :
            TypedResults.NotFound(result.Error);
    }
}
