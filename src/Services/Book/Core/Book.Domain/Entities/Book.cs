using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Book : AggregateRoot
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


    private readonly List<Category> _category = [];

    public IReadOnlyList<Category> Categories => _category.AsReadOnly();


    private readonly List<Author> _author = [];

    public IReadOnlyList<Author> Authors => _author.AsReadOnly();


    private readonly List<Genre> _genre = [];

    public IReadOnlyList<Genre> Genres => _genre.AsReadOnly();

    private Book(
        Guid id, 
        string title, 
        string description, 
        decimal price, 
        DateTimeOffset publicationDate, 
        bool isAvailable, 
        short pages, 
        AgeRating ageRating) : base(id)
    {
        Title = title;
        Description = description;
        Price = price;
        PublicationDate = publicationDate;
        IsAvailable = isAvailable;
        Pages = pages;
        AgeRating = ageRating;
    }

    public static Book Create(
        string title, 
        string description, 
        decimal price, 
        DateTimeOffset publicationDate, 
        bool isAvailable, 
        short pages, 
        AgeRating ageRating)
    {
        var book = new Book(
            Guid.NewGuid(), 
            title, 
            description, 
            price, 
            publicationDate, 
            isAvailable,
            pages, 
            ageRating);

        book.Validate();

        return book;
    }

    public void Update(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

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

    public override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));

        if (Title.Length > TitleMaxLength)
            throw new ArgumentException($"Название книги должно быть не больше " +
                $"{TitleMaxLength} количества слов.", nameof(Title));

        if (Description.Length > DescriptionMaxLength)
            throw new ArgumentException($"Описание не может содержать больше " +
                $"{DescriptionMaxLength} слов.", nameof(Description));

        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Pages, nameof(Pages));

        if (!Enum.IsDefined(typeof(AgeRating), AgeRating))
            throw new ArgumentException("Недопустимое значение возрастного рейтинга.",
                nameof(AgeRating));
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