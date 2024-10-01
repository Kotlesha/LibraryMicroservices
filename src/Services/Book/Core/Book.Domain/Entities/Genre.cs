﻿using Shared.CleanArchitecture.Domain.Entities;

namespace Book.Domain.Entities;

public sealed class Genre : AggregateRoot
{
    private const int NameMaxLength = 100;

    public string Name { get; private set; }

    private readonly List<Book> _books = [];

    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private readonly List<Category> _categories = [];

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private Genre(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public static Genre Create(string name)
    {
        var genre = new Genre(Guid.NewGuid(), name);

        genre.Validate();

        return genre;
    }

    public void Update(Genre genre)
    {
        ArgumentNullException.ThrowIfNull(genre, nameof(genre));

        Name = genre.Name;
    }

    public override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));

        if (Name.Length > NameMaxLength)
            throw new ArgumentException($"Название жанра должно быть не больше " +
                $"{NameMaxLength} количества слов.", nameof(Name));
    }
}