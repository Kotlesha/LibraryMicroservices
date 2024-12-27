using Shared.CleanArchitecture.Domain.Entities;
using System.Text.Json.Serialization;

namespace Book.Domain.Entities;

public sealed class Category : AggregateRoot
{
    public string Name { get; private set; }

    [JsonIgnore]
    private readonly List<Book> _books = [];
    [JsonIgnore]
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

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

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
    }
}