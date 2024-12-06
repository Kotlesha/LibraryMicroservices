using Swashbuckle.AspNetCore.Filters;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Examples.User.GetUserById;

public class GetUserByIdSuccessResponseExample : IExamplesProvider<UserDTO>
{
    public UserDTO GetExamples()
    {
        return new("Алексей",
            "Кот",
            "Владимирович",
            new DateOnly(2001, 10, 25),
            "kot@gmail.com",
            Guid.NewGuid());
    }
}
