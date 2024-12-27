namespace Order.Application.DTOs.RequestDTOs;

public sealed record BookRequestDTO(
    string Title,
    decimal Price);
