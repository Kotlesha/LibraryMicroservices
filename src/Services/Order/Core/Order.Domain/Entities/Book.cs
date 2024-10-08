using Shared.CleanArchitecture.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Order.Domain.Entities;

public sealed class Book : AggregateRoot
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    private readonly List<Order> _orders = [];
    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    private Book(Guid id, string title, decimal price) : base(id)
    {
        Title = title;
        Price = price;
    }

    public static Book Create(string title, decimal price = 0.0m) 
    { 
        var book = new Book(Guid.NewGuid(), title, price);
        book.Validate();

        return book;
    }

    public void Update(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));
        book.Validate();

        Title = book.Title;
        Price = book.Price;  
    }

    public void MakeAvailable() => IsAvailable = true;
    public void MakeUnAvailable() => IsAvailable = false;

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));
        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
    }
}
