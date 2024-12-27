using Shared.CleanArchitecture.Domain.Entities;
using System.Text.Json.Serialization;

namespace Book.Domain.Entities;

public sealed class Author : AggregateRoot
{
    public string? Surname { get; private set; }
    public string Name { get; private set; }

    private readonly List<Book> _books = [];

    [JsonIgnore]
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    [JsonConstructor]
    private Author(Guid id, string name, string? surname) : base(id)
    {
        Surname = surname;
        Name = name;
    }

    public static Author Create(string name, string? surname)
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

    public override string ToString() => $"{Name} {Surname}";

    public override bool Equals(object? obj)
    {
        if (obj is not Author other)
            return false;

        return Id.Equals(other.Id) &&
               string.Equals(Name, other.Name, StringComparison.Ordinal) &&
               string.Equals(Surname, other.Surname, StringComparison.Ordinal);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Surname);
    }
}