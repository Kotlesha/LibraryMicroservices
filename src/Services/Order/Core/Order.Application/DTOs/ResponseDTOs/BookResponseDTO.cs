namespace Order.Application.DTOs.ResponseDTOs;
public sealed record BookResponseDTO(
    Guid Id,
    string Title,
    decimal Price,
    bool IsAvailable);
