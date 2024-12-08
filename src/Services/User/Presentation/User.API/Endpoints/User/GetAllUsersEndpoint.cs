using MediatR;
using User.API.Metadata.User;
using User.Application.Features.User.Queries.GetAll;

namespace User.API.Endpoints.User;

public static class GetAllUsersEndpoint
{
    public static void MapGetAllUsersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/",
            async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var users = await sender.Send(
                    new GetAllUsersQuery(), 
                    cancellationToken);

                return users.Any() ?
                    Results.Ok(users) :
                    Results.NoContent();
            })
        .ApplyGetAllUsersMetadata();
    }
}
