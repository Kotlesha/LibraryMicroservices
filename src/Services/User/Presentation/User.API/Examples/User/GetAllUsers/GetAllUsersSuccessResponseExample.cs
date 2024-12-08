using Swashbuckle.AspNetCore.Filters;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Examples.User.GetAllUsers;

public class GetAllUsersSuccessResponseExample : IExamplesProvider<IEnumerable<UserDTO>>
{
    public IEnumerable<UserDTO> GetExamples()
    {
        return [
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
        ];
    }
}
