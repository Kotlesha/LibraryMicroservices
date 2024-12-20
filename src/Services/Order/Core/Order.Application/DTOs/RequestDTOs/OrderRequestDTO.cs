namespace Order.Application.DTOs.RequestDTOs;
public sealed record OrderRequestDTO(
    decimal TotalCost,
    IEnumerable<Guid> BooksIds);
