﻿using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Book : AggregateRoot
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }

    private readonly List<Order> _orders = [];
    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    private Book(Guid id, string title, decimal price) : base(id)
    {
        Title = title;
        Price = price;
    }

    public static Book Create(string title, decimal price) 
    { 
        var book = new Book(Guid.NewGuid(), title, price);
        book.Validate();

        return book;
    }

    public void Update(Book book)
    {
        Title = book.Title;
        Price = book.Price;
        ArgumentNullException.ThrowIfNull(book, nameof(book));
        book.Validate();

    }

    public override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));
        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
    }
}
