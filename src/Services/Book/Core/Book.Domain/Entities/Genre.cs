using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Genre : AggregateRoot<Guid>
{
    private const int NameMaxLength = 100;

    public string Name { get; private set; }

    public Guid BookId { get; private set; }

    public Book Book { get; private set; }

    private readonly List<Category> _categories = [];

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private Genre(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public static Genre Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (name.Length > NameMaxLength)
            throw new ArgumentException($"Название жанра должно быть не больше " +
                $"{NameMaxLength} количества слов.", nameof(name));

        return new(Guid.NewGuid(), name);
    }

    public void Update(Genre genre)
    {
        Id = genre.Id;
        Name = genre.Name;
    }
}