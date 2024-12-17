using Order.Domain.Entities;
using Order.Domain.Enums;

namespace Order.Application.DTOs.ResponseDTOs;

public sealed record OrderResponseDTO(
    decimal TotalCost,
    Status Status,
    IEnumerable<Book> Books);
