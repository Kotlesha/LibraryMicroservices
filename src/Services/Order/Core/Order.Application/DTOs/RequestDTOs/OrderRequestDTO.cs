namespace Order.Application.DTOs.RequestDTOs;

public sealed record OrderRequestDTO(
    IEnumerable<Guid> BooksIds);