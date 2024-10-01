using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Book : AggregateRoot<Guid>
{

    public string Title {  get; private set; }

    public decimal Price {  get; private set; }

    private readonly List<Order> _orders = [];
    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    private Book(Guid id, string title, decimal price)
    {
        Id = id;
        Title = title;
        Price = price;
    }

    public static Book Create(string title, decimal price) 
    { 
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));
        return new(Guid.NewGuid(), title, price);
    }

    public void Update(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Price = book.Price;
    }
}
