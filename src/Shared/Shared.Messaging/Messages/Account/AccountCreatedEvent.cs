namespace Shared.Messaging.Messages.Account;

public record AccountCreatedEvent(
    Guid AccountId,
    string Name,
    string Surname,
    string Patronymic,
    DateOnly? BirthDate,
    string Email);
