using Swashbuckle.AspNetCore.Filters;
using User.Application.Features.User.Commands.Create;

namespace User.API.Examples.User.CreateUser;

public class CreateUserCommandExample : IExamplesProvider<CreateUserCommand>
{
    public CreateUserCommand GetExamples()
    {
        return new CreateUserCommand(
            "Алексей",
            "Кот",
            "Владимирович",
            new DateOnly(2001, 10, 25),
            "kot@gmail.com",
            Guid.NewGuid());
    }
}
