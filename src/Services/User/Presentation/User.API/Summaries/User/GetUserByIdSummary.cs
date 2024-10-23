using FastEndpoints;
using User.API.Endpoints.User;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Summaries.User;

public sealed class GetUserByIdSummary : Summary<GetUserByIdEndpoint>
{
    public GetUserByIdSummary()
    {
        Summary = "Retrieves a user's information by their ID";
        Description = "This endpoint fetches detailed information about a user by their unique identifier (userId). If the user is not found, a NotFound response is returned.";

        Params["userId"] = "The unique identifier of the user (GUID format)";

        Response(200, "User information", example: new UserDTO(
            Name: "Олег",
            Surname: "Кузнецов",
            Patronymic: "Иванович",
            BirthDate: new DateOnly(1988, 12, 5),
            Email: "oleg.kuznetsov@example.com",
            ApplicationUserId: Guid.NewGuid()
        ));

        Response(404, "User not found", example: ApplicationErrors.User.NotFound);
    }
}
