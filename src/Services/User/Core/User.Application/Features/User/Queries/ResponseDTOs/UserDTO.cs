namespace User.Application.Features.User.Queries.ResponseDTOs;

public sealed record UserDTO(
    string Name,
    string Surname,
    string Patronymic,
    DateOnly BirthDate,
    string Email,
    Guid ApplicationUserId);
