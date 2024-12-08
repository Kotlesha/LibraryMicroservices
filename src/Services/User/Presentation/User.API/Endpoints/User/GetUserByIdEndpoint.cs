using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Common.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using User.API.Metadata.User;
using User.Application.Features.User.Queries.GetById;

namespace User.API.Endpoints.User;

public static class GetUserByIdEndpoint
{
    public static void MapGetUserByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{applicationUserId}",
            async (
                [FromRoute, SwaggerParameter("The unique identifier of the user (GUID format)")] Guid applicationUserId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetUserByIdQuery(applicationUserId),
                    cancellationToken);

                return result.IsSuccess ?
                    Results.Ok(result.Value) :
                    result.ToProblemDetails();
            })
        .ApplyGetUserByIdMetadata();
    }
}
