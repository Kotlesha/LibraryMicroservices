namespace Book.Application.DTOs.ResponseDTOs;

public sealed record BookResponseDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public short Pages { get; init; }
    public DateTime PublicationDateUtc { get; init; }
    public bool IsAvailable { get; init; }
    public string AgeRating { get; init; }
    public string ISBN { get; init; }
    public Guid CategoryId { get; init; }
    public IEnumerable<Guid> AuthorsIds { get; init; } = [];
    public IEnumerable<Guid> GenresIds { get; init; } = [];

    private BookResponseDTO() { }

    public BookResponseDTO(
        Guid id,
        string title,
        string? description,
        decimal price,
        short pages,
        DateTime publicationDateUtc,
        bool isAvailable,
        string ageRating,
        string isbn,
        Guid categoryId,
        IEnumerable<Guid> authorsIds,
        IEnumerable<Guid> genresIds)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        Pages = pages;
        PublicationDateUtc = publicationDateUtc;
        IsAvailable = isAvailable;
        AgeRating = ageRating;
        ISBN = isbn;
        CategoryId = categoryId;
        AuthorsIds = authorsIds;
        GenresIds = genresIds;
    }
}
