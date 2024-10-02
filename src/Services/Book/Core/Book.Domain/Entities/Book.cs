using Book.Domain.Enums;
using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Book : AggregateRoot
{
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
        short pages, 
        AgeRating ageRating) : base(id)
    {
        Title = title;
        Description = description;
        Price = price;
        PublicationDate = publicationDate;
        Pages = pages;
        AgeRating = ageRating;
    }

    public static Book Create(
        string title, 
        string description, 
        decimal price, 
        DateTimeOffset publicationDate, 
        short pages, 
        AgeRating ageRating)
    {
        var book = new Book(
            Guid.NewGuid(), 
            title, 
            description, 
            price, 
            publicationDate, 
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
        Pages = book.Pages;
        AgeRating = book.AgeRating;
    }

    public void MakeAvailable() => IsAvailable = true;

    public void MakeUnavailable() => IsAvailable = false;

    public override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));
        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Pages, nameof(Pages));
    }
}