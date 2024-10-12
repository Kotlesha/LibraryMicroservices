using Book.Domain.Errors;
using Shared.CleanArchitecture.Common;
using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Category : AggregateRoot
{
    public string Name { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private readonly List<Genre> _genres = [];
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    private Category(Guid id, string name) : base(id) => Name = name;

    public static Category Create(string name)
    {
        var category = new Category(Guid.NewGuid(), name);
        category.Validate();

        return category;
    }

    public void Update(Category category)
    {
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        category.Validate();

        Name = category.Name;
    }

    private bool HasBook(Book book) => _books.Any(b => b.Id.Equals(book.Id));
    private bool HasGenre(Genre genre) => _genres.Any(g => g.Id.Equals(genre.Id));

    public Result AddBookToCategory(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        if (HasBook(book))
        {
            Result.Failure(DomainErrors.Category.BookAlreadyExists);
        }

        _books.Add(book);
        return Result.Success();
    }

    public Result RemoveBookFromCategory(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        if (!HasBook(book))
        {
            Result.Failure(DomainErrors.Category.BookNotFound);
        }

        _books.Remove(book);
        return Result.Success();
    }

    public Result AddGenreToCategory(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre, nameof(genre));

        if (HasGenre(genre))
        {
            Result.Failure(DomainErrors.Category.GenreAlreadyExists);
        }

        _genres.Add(genre);
        return Result.Success();
    }

    public Result RemoveGenreFromCategory(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre, nameof(genre));

        if (!HasGenre(genre))
        {
            Result.Failure(DomainErrors.Category.GenreNotFound);
        }

        _genres.Remove(genre);
        return Result.Success();
    }

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
    }
}