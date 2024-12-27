namespace Book.Application.DTOs.RequestDTOs;

public sealed record AuthorRequestDTO(
    string? Surname, 
    string Name);