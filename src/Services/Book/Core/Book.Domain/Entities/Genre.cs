using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Genre : AggregateRoot
{
    public string Name { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private readonly List<Category> _categories = [];
    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private Genre(Guid id, string name) : base(id) => Name = name;

    public static Genre Create(string name)
    {
        var genre = new Genre(Guid.NewGuid(), name);
        genre.Validate();

        return genre;
    }

    public void Update(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre, nameof(genre));
        genre.Validate();

        Name = genre.Name;
    }

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
    }
}