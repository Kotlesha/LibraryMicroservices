using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace User.Application.Features.User.Commands.Create;

public sealed record CreateUserCommand(
    string Name,
    string Surname,
    string Patronymic,
    DateOnly BirthDate,
    string Email,
    Guid ApplicationUserId) : ICommand<Guid>;
