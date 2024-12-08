using Swashbuckle.AspNetCore.Filters;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.API.Examples.Common;

public class GetUserSuccessResponseExample : IExamplesProvider<UserDTO>
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
