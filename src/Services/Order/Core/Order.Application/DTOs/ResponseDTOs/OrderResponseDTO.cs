namespace Order.Application.DTOs.ResponseDTOs;

public sealed record OrderResponseDTO(
    Guid OrderId,
    DateTime CreatedTimeUtc,
    decimal TotalCost,
    string Status,
    DateTime? CanceledTimeUtc,
    List<BookResponseDTO> Books);
