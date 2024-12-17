namespace Auth.BLL.DTOs.RequestDTOs;

public sealed record RegisterDTO(
    string Email,
    string Password,
    string PasswordConfirmation);
