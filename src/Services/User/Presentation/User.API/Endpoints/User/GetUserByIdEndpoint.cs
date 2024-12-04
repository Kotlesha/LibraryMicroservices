using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.CleanArchitecture.Common.Extensions;
using User.Application.Features.User.Queries.GetById;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Endpoints.User;

public sealed class GetUserByIdEndpoint(ISender sender) 
    : Endpoint<GetUserByIdQuery, Results<Ok<UserDTO>, ProblemHttpResult>>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("{userId:guid}");

        Description(e => {
            e.WithTags("Users");
            e.WithName("GetUserByIdEndpoint");
        });

        AllowAnonymous();
    }

    public override async Task<Results<Ok<UserDTO>, ProblemHttpResult>> ExecuteAsync(
        GetUserByIdQuery req, CancellationToken ct)
    {
        var result = await _sender.Send(req, ct);

        return result.IsSuccess ?
            TypedResults.Ok(result.Value) :
            TypedResults.Problem(result.Error);
    }
}
