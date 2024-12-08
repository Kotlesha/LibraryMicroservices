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
                Surname: "Кот",
                Patronymic: "Владимирович",
                BirthDate: new DateOnly(1985, 3, 15),
                Email: "kot1@gmail.com",
                ApplicationUserId: Guid.NewGuid()
            ),
            new UserDTO(
                Name: "Андрей",
                Surname: "Кот",
                Patronymic: "Александрович",
                BirthDate: new DateOnly(1985, 3, 15),
                Email: "kot@gmail.com",
                ApplicationUserId: Guid.NewGuid()
            ),
        ];
    }
}
