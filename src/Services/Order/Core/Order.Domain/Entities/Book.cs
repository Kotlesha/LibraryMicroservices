using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Book : AggregateRoot
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }

    private readonly List<Order> _orders = [];
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private Book(Guid id, string title, decimal price, bool isAvailable) : base(id)
    {
        Title = title;
        Price = price;
        IsAvailable = isAvailable;
    }

    public static Book Create(Guid id, string title, decimal price = 0.0m, bool isAvailable = true)
    {
        var book = new Book(id, title, price, isAvailable);
        book.Validate();

        return book;
    }

    public static Book Create(string title, decimal price = 0.0m, bool isAvailable = true) 
    { 
        var book = new Book(Guid.NewGuid(), title, price, isAvailable);
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

    public override bool Equals(object? obj)
    {
        if (obj is not Book other)
        {
            return false;
        }

        return Id == other.Id &&
               Title == other.Title &&
               Price == other.Price;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Price);
    }

    public void MakeAvailable() => IsAvailable = true;
    public void MakeUnAvailable() => IsAvailable = false;

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Title, nameof(Title));
        ArgumentOutOfRangeException.ThrowIfNegative(Price, nameof(Price));
    }
}
