using Order.Domain.Entities;
using Order.Domain.Enums;

namespace Order.Application.DTOs.ResponseDTOs;

public class OrderResponseDTO(
    Guid UserId,
    DateTime CreatedTineUtc,
    decimal TotalCost,
    Status Status,
    IEnumerable<Book> Books);
