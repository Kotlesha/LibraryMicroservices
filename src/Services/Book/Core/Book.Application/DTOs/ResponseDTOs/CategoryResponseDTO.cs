namespace Book.Application.DTOs.ResponseDTOs;

public sealed record CategoryResponseDTO(
    Guid Id,
    string Name);