using Swashbuckle.AspNetCore.Filters;
using System.Security.Cryptography;
using User.Application.Features.User.Commands.Create;

namespace User.API.Examples.User.CreateUser;

public class CreateUserCommandExample : IExamplesProvider<CreateUserCommand>
{
    public CreateUserCommand GetExamples()
    {
        var number = RandomNumberGenerator.GetInt32(int.MaxValue);

        return new CreateUserCommand(
            "Алексей",
            "Кот",
            "Владимирович",
            new DateOnly(2001, 10, 25),
            $"kot{number}@gmail.com",
            Guid.NewGuid());
    }
}
