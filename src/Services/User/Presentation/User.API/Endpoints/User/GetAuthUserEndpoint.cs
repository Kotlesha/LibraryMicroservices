using MediatR;
using Shared.CleanArchitecture.Common.Extensions;
using User.API.Metadata.User;
using User.Application.Features.User.Queries.GetAuth;

namespace User.API.Endpoints.User;

public static class GetAuthUserEndpoint
{
    public static void MapGetAuthUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/me",
            async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetAuthUserQuery(),
                    cancellationToken);

                return result.IsSuccess ?
                    Results.Ok(result.Value) :
                    result.ToProblemDetails();
            })
        .ApplyGetAuthUserMetadata();
    }
}

