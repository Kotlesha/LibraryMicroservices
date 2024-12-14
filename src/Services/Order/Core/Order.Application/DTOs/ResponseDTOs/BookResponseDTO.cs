namespace Order.Application.DTOs.ResponseDTOs
{
    public class BookResponseDTO(
        Guid Id,
        string Title,
        decimal Price,
        bool IsAvailable);
}
