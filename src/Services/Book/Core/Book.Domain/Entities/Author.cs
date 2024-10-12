using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Author : AggregateRoot
{
    public string Surname { get; private set; }
    public string Name { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private Author(Guid id, string surname, string name) : base(id)
    {
        Surname = surname;
        Name = name;
    }

    public static Author Create(string surname, string name)
    {
        var author = new Author(Guid.NewGuid(), surname, name);
        author.Validate();

        return author;
    }

    public void Update(Author author)
    {
        ArgumentNullException.ThrowIfNull(author, nameof(author));
        author.Validate();

        Surname = author.Surname;
        Name = author.Name;
    }

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Surname, nameof(Surname));
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
    }
}