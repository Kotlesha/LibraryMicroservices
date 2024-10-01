using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Book : AggregateRoot<Guid>
{
    private const int TitleMaxLength = 500;
    private const int DescriptionMaxLength = 1000;

    public string Title {get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public DateTimeOffset PublicationDate { get; private set; }

    public bool IsAvailable { get; private set; } = true;

    public short Pages { get; private set; }

    public AgeRating AgeRating { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public Guid AuthorId { get; private set; }

    public Author Author { get; private set; }

    public Guid GenreId { get; private set; }

    public Genre Genre { get; private set; }

    private Book(Guid id, string title, string description, decimal price, 
        DateTimeOffset publicationDate, bool isAvailable, short pages, AgeRating ageRating)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        PublicationDate = publicationDate;
        IsAvailable = isAvailable;
        Pages = pages;
        AgeRating = ageRating;
    }

    public static Book Create(string title, string description, decimal price, 
        DateTimeOffset publicationDate, bool isAvailable, short pages, AgeRating ageRating)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

        if (title.Length > TitleMaxLength)
            throw new ArgumentException($"Название книги должно быть не больше " +
                $"{TitleMaxLength} количества слов.", nameof(title));

        if (description.Length > DescriptionMaxLength)
            throw new ArgumentException($"Описание не может содержать больше " +
                $"{DescriptionMaxLength} слов.", nameof(description));

        ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pages, nameof(pages));

        if (!Enum.IsDefined(typeof(AgeRating), ageRating))
            throw new ArgumentException("Недопустимое значение возрастного рейтинга.", 
                nameof(ageRating));

        return new(Guid.NewGuid(), title, description, price, publicationDate, isAvailable, 
            pages, ageRating);
    }

    public void Update(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Description = book.Description;
        Price = book.Price;
        PublicationDate = book.PublicationDate;
        IsAvailable = book.IsAvailable;
        Pages = book.Pages;
        AgeRating = book.AgeRating;
    }

    public string GetAgeRatingString()
    {
        return $"{(byte)AgeRating}+";
    }
}
public enum AgeRating : byte
{
    ZeroPlus = 0,
    SixPlus = 6,
    TwelvePlus = 12,
    SixteenPlus = 16,
    EighteenPlus = 18
}