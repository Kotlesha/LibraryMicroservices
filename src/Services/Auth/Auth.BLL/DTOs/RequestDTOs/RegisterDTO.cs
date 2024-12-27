namespace Auth.BLL.DTOs.RequestDTOs;

public sealed record RegisterDTO(
    string Name,
    string Surname,
    string Patronymic,
    DateOnly? BirthDate,
    string Email,
    string Password,
    string PasswordConfirmation);
