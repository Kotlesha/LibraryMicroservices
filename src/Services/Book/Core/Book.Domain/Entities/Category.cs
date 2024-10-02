using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Category : AggregateRoot
{
    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private readonly List<Genre> _genries = [];
    public IReadOnlyList<Genre> Genries => _genries.AsReadOnly();

    public string Name { get; private set; }

    private Category(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public static Category Create(string name)
    {
        var category = new Category(Guid.NewGuid(), name);
        category.Validate();

        return category;
    }

    public void Update(Category category)
    {
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        Name = category.Name;
    }

    public override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
    }
}
