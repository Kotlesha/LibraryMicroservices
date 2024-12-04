using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.CleanArchitecture.Common;
using Shared.CleanArchitecture.Common.Components;
using User.Application.Features.User.Queries.GetAuth;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Endpoints.User;

public sealed class GetAuthUserEndpoint(ISender sender) 
    : EndpointWithoutRequest<Results<Ok<UserDTO>, BadRequest<Error>>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("/me");
        Description(e => e.WithTags("Users"));
        AllowAnonymous();
    }

    public override async Task<Results<Ok<UserDTO>, BadRequest<Error>>> ExecuteAsync(CancellationToken ct)
    {
        var result = await _sender.Send(new GetAuthUserQuery(), ct);

        return result.IsSuccess ?
            TypedResults.Ok(result.Value) :
            TypedResults.BadRequest(result.Error);
    }
}
