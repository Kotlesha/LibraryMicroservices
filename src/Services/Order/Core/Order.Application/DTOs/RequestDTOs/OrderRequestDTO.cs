namespace Order.Application.DTOs.RequestDTOs;

public sealed record OrderRequestDTO(
    DateTime CreatedTimeUtc,
    decimal TotalCost,
    IEnumerable<Guid> BooksIds);
