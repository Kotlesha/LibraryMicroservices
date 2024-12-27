using Shared.CleanArchitecture.Domain.Entities;
using System.Text.Json.Serialization;

namespace Book.Domain.Entities;

public sealed class Genre : AggregateRoot
{
    public string Name { get; private set; }

    private readonly List<Book> _books = [];

    [JsonIgnore]
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    [JsonConstructor]
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

    public override string ToString() => $"{Name}";

    public override bool Equals(object? obj)
    {
        if (obj is not Genre other)
            return false;

        return Id.Equals(other.Id) &&
               string.Equals(Name, other.Name, StringComparison.Ordinal);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}