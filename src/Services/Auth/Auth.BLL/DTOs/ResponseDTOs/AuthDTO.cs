namespace Auth.BLL.DTOs.ResponseDTOs;

public sealed record AuthDTO(string AccessToken, string RefreshToken);
