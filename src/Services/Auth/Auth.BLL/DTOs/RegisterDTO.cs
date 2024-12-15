namespace Auth.BLL.DTOs;

public sealed record RegisterDTO(
    string Email, 
    string Password,
    string PasswordConfirmation);
