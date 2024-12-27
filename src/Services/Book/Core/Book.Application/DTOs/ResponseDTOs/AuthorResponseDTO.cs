namespace Book.Application.DTOs.ResponseDTOs;

public sealed record AuthorResponseDTO(
    Guid Id,
    string? Surname,
    string Name);