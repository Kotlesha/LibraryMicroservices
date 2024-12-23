namespace Order.Application.DTOs.ResponseDTOs;

public sealed record OrderResponseDTO(
    Guid OrderId,
    DateTime CreatedTimeUtc,
    int Count,
    decimal TotalCost,
    string Status,
    DateTime? CanceledTimeUtc,
    List<BookResponseDTO> Books);
