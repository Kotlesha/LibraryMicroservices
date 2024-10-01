using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Author : AggregateRoot<Guid>
{
    private const int NameMaxLength = 100;
    private const int SurnameMaxLength = 100;

    public string Surname { get; private set; }

    public string Name { get; private set; }

    public Guid BookId { get; private set; }

    public Book Book { get; private set; }

    private Author(Guid id, string surname, string name)
    {
        Id = id;
        Surname = surname;
        Name = name;
    }

    public static Author Create(string surname, string name)
    {
        if (surname.Length > SurnameMaxLength)
            throw new ArgumentException($"Фамилия автора должна быть не больше " +
                $"{SurnameMaxLength} количеств слов.", nameof(surname));

        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (name.Length > NameMaxLength)
            throw new ArgumentException($"Имя автора должно быть не больше " +
                $"{NameMaxLength} количества слов.", nameof(name));

        return new(Guid.NewGuid(), surname, name);
    }

    public void Update(Author author)
    {
        Id = author.Id;
        Surname = author.Surname;
        Name = author.Name;
    }
}