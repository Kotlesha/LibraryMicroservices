using Book.Domain.Enums;
using Book.Domain.Errors;
using Shared.CleanArchitecture.Common.Components.Results;
using Shared.CleanArchitecture.Domain.Entities;

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
    public string ISBN { get; private set; }

    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    private readonly List<Author> _authors = [];
    public IReadOnlyList<Author> Authors => _authors.AsReadOnly();

    private readonly List<Genre> _genres = [];
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    private Book(
        Guid id, 
        string title, 
        string description, 
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
        ISBN = isbn;
        CategoryId = categoryId;
    }

    public static Book Create(
        string title,
        string description,
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
        ISBN = book.ISBN;
        CategoryId = book.CategoryId;
    }

    public void MakeAvailable() => IsAvailable = true;
    public void MakeUnavailable() => IsAvailable = false;

    private bool HasAuthor(Author author) => _authors.Any(a => a.Id.Equals(author.Id));
    private bool HasGenre(Genre genre) => _genres.Any(g => g.Id.Equals(genre.Id));

    public Result AddAuthorToBook(Author author)
    {
        ArgumentNullException.ThrowIfNull(author, nameof(author));

        if (HasAuthor(author))
        {
            Result.Failure(DomainErrors.Book.AuthorAlreadyExists);
        }

        _authors.Add(author);
        return Result.Success();
    }

    public Result RemoveAuthorFromBook(Author author)
    {
        ArgumentNullException.ThrowIfNull(author, nameof(author));

        if (!HasAuthor(author))
        {
            Result.Failure(DomainErrors.Book.AuthorNotFound);
        }

        _authors.Remove(author);
        return Result.Success();
    }

    public Result AddGenreToBook(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre, nameof(genre));

        if (HasGenre(genre))
        {
            Result.Failure(DomainErrors.Book.GenreAlreadyExists);
        }

        _genres.Add(genre);
        return Result.Success();
    }

    public Result RemoveGenreFromBook(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre, nameof(genre));

        if (!HasGenre(genre))
        {
            Result.Failure(DomainErrors.Book.GenreNotFound);
        }

        _genres.Remove(genre);
        return Result.Success();
    }

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));
        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Pages, nameof(Pages));
        ArgumentException.ThrowIfNullOrWhiteSpace(ISBN, nameof(ISBN));
    }
}