using Book.Domain.Enums;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Helpers;
using System.Text.Json.Serialization;

namespace Book.Domain.Entities;

public sealed class Book : AggregateRoot
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public DateTime PublicationDateUtc { get; private set; }
    public bool IsAvailable { get; private set; }
    public short Pages { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public string Isbn { get; private set; }

    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    [JsonInclude]
    private List<Author> _authors = [];

    [JsonIgnore]
    public IReadOnlyList<Author> Authors => _authors.AsReadOnly();

    [JsonInclude]
    private List<Genre> _genres = [];

    [JsonIgnore]
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    [JsonConstructor]
    private Book(
        Guid id, 
        string title, 
        string? description, 
        decimal price, 
        DateTime publicationDateUtc, 
        bool isAvailable,
        short pages, 
        AgeRating ageRating,
        string isbn,
        Guid? categoryId) : base(id)
    {
        Title = title;
        Description = description;
        Price = price;
        PublicationDateUtc = publicationDateUtc;
        IsAvailable = isAvailable;
        Pages = pages;
        AgeRating = ageRating;
        Isbn = isbn;
        CategoryId = categoryId;
    }

    public static Book Create(
        string title,
        string? description,
        decimal price,
        short pages,
        AgeRating ageRating,
        string isbn,
        Guid? categoryId,
        bool isAvailable = true)
    {
        var book = new Book(
            Guid.NewGuid(), 
            title, 
            description, 
            price, 
            DateTime.UtcNow,
            isAvailable,
            pages, 
            ageRating,
            isbn,
            categoryId);

        book.Validate();

        return book;
    }

    public void Update(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));
        book.Validate();

        Title = book.Title;
        Description = book.Description;
        Price = book.Price;
        PublicationDateUtc = book.PublicationDateUtc;
        Pages = book.Pages;
        AgeRating = book.AgeRating;
        Isbn = book.Isbn;
        CategoryId = book.CategoryId;
    }

    public void MakeAvailable() => IsAvailable = true;
    public void MakeUnavailable() => IsAvailable = false;

    public void AddAuthorToBook(Author author) => AddEntity(author, _authors);
    public void AddGenreToBook(Genre genre) => AddEntity(genre, _genres);

    public void RemoveAuthorFromBook(Author author) => RemoveEntity(author, _authors);
    public void RemoveGenreFromBook(Genre genre) => RemoveEntity(genre, _genres);

    public void UpdateAuthors(IEnumerable<Author> authors)
    {
        EntityUpdater.UpdateRelatedEntities(
            _authors,
            authors,
            AddAuthorToBook,
            RemoveAuthorFromBook);
    }

    public void UpdateGenres(IEnumerable<Genre> genres)
    {
        EntityUpdater.UpdateRelatedEntities(
            _genres,
            genres,
            AddGenreToBook,
            RemoveGenreFromBook);
    }

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));
        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Pages, nameof(Pages));
        ArgumentException.ThrowIfNullOrWhiteSpace(Isbn, nameof(Isbn));
    }

    public override string ToString() => $"{Title} {Description} {Price} {Pages} {AgeRating} {Isbn} {CategoryId} {IsAvailable}";
}