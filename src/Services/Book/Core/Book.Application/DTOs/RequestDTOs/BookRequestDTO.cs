using Book.Domain.Enums;

namespace Book.Application.DTOs.RequestDTOs;

public sealed record BookRequestDTO(
    string Title, 
    string? Description,
    decimal Price,
    short Pages,
    AgeRating AgeRating,
    string ISBN,
    Guid CategoryId,
    IEnumerable<Guid> AuthorsIds,
    IEnumerable<Guid> GenresIds);