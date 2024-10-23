using FastEndpoints;
using User.API.Endpoints.User;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Summaries.User;

public sealed class GetAuthUserSummary : Summary<GetAuthUserEndpoint>
{
    public GetAuthUserSummary()
    {
        Summary = "Gets the authenticated user's information";
        Description = "This endpoint retrieves information about the currently authenticated user. If the request is invalid, a BadRequest response is returned.";

        Response(200, "Authenticated user information", example: new UserDTO(
            Name: "Иван",
            Surname: "Сидоров",
            Patronymic: "Александрович",
            BirthDate: new DateOnly(1995, 10, 20),
            Email: "ivan.sidorov@example.com",
            ApplicationUserId: Guid.NewGuid()
        ));

        Response(400, "Invalid request", example: ApplicationErrors.User.InvalidUserIdFromat);
    }
}
