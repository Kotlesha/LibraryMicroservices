using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Category : AggregateRoot<Guid>
{
    private const int NameMaxLength = 50;

    private readonly List<Book> _books = [];

    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private readonly List<Genre> _genries = [];

    public IReadOnlyList<Genre> Genries => _genries.AsReadOnly();

    public string Name { get; private set; }

    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (name.Length > NameMaxLength)
            throw new ArgumentException($"Название категории должно быть не больше " +
                $"{NameMaxLength} количества слов.", nameof(name));

        return new(Guid.NewGuid(), name);
    }

    public void Update(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}
