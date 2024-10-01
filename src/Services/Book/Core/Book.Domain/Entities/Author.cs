using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Author : AggregateRoot
{
    private const int NameMaxLength = 100;
    private const int SurnameMaxLength = 100;

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

        Surname = author.Surname;
        Name = author.Name;
    }

    public override void Validate()
    {
        if (Surname.Length > SurnameMaxLength)
            throw new ArgumentException($"Фамилия автора должна быть не больше " +
                $"{SurnameMaxLength} количеств слов.", nameof(Surname));

        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));

        if (Name.Length > NameMaxLength)
            throw new ArgumentException($"Имя автора должно быть не больше " +
                $"{NameMaxLength} количества слов.", nameof(Name));
    }
}