using FastEndpoints;
using User.API.Endpoints.User;
using User.Application.Errors;
using User.Application.Features.User.Commands.Create;

namespace User.API.Summaries.User;

public sealed class CreateUserSummary : Summary<CreateUserEndpoint>
{
    public CreateUserSummary()
    {
        Summary = "Creates a new user";
        Description = "This endpoint allows for the creation of a new user by sending a CreateUserCommand.";

        ExampleRequest = new CreateUserCommand(
            Name: "Алексей",
            Surname: "Кот",
            Patronymic: "Владимирович",
            BirthDate: new DateOnly(2001, 10, 25),
            Email: "kot@gmail.com",
            ApplicationUserId: Guid.NewGuid()
        );

        Response(201, "User successfully created", example: Guid.NewGuid());

        Response(409, "Conflict: a user with this ApplicationUserId already exists.",
            example: ApplicationErrors.User.ApplicationUserIdAlreadyExists);
    }
}
