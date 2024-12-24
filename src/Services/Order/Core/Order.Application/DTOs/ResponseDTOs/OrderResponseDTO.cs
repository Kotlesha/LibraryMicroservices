namespace Order.Application.DTOs.ResponseDTOs;

public sealed class OrderResponseDTO
{
    public Guid OrderId { get; init; }
    public DateTime CreatedTimeUtc { get; init; }
    public int Count { get; init; }
    public decimal TotalCost { get; init; }
    public string Status { get; init; }
    public DateTime? CanceledTimeUtc { get; init; }
    public IEnumerable<BookResponseDTO> Books { get; init; }

    public OrderResponseDTO()
    {
        Books = [];
    }

    public OrderResponseDTO(Guid orderId, DateTime createdTimeUtc, int count, decimal totalCost, string status, DateTime? canceledTimeUtc, IEnumerable<BookResponseDTO> books)
    {
        OrderId = orderId;
        CreatedTimeUtc = createdTimeUtc;
        Count = count;
        TotalCost = totalCost;
        Status = status;
        CanceledTimeUtc = canceledTimeUtc;
        Books = books ?? new List<BookResponseDTO>();
    }
}
