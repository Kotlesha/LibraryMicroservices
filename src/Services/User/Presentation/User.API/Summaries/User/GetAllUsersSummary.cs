using FastEndpoints;
using User.API.Endpoints.User;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Summaries.User;

public sealed class GetAllUsersSummary : Summary<GetAllUsersEndpoint>
{
    public GetAllUsersSummary()
    {
        Summary = "Retrieves a list of all users";
        Description = "This endpoint returns a list of all registered users. If no users are found, it will return a NoContent response.";

        Response<IEnumerable<UserDTO>>(200, "List of users", example:
        [
            new UserDTO(
                Name: "Алексей",
                Surname: "Иванов",
                Patronymic: "Сергеевич",
                BirthDate: new DateOnly(1985, 3, 15),
                Email: "alexey.ivanov@example.com",
                ApplicationUserId: Guid.NewGuid()
            ),
            new UserDTO(
                Name: "Мария",
                Surname: "Петрова",
                Patronymic: "Ивановна",
                BirthDate: new DateOnly(1990, 7, 21),
                Email: "maria.petrova@example.com",
                ApplicationUserId: Guid.NewGuid()
            )
        ]);

        Response(204, "No users found");
    }
}
