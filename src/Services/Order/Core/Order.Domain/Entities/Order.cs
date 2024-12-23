using Order.Domain.Enums;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.CleanArchitecture.Domain.Helpers;

namespace Order.Domain.Entities;

public sealed class Order : AggregateRoot
{
    public Guid UserId { get; private set; }
    public DateTime CreatedTimeUtc { get; private set; }
    public int Count { get; private set; }
    public decimal TotalCost { get; private set; }
    public Status Status { get; private set; }
    public DateTime? CanceledTimeUtc { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    public void AddBookToOrder(Book book)
    {
        AddEntity(book, _books);
        TotalCost += book.Price;
        Count++;
    }

    public void RemoveBookFromOrder(Book book) 
    {   RemoveEntity(book, _books);
        TotalCost -= book.Price;
        Count--;
    }

    private Order(Guid Id, Guid userId, int count = 0, decimal totalCost = 0.0m, Status status = Status.Active) : base(Id)
    {
        UserId = userId;
        CreatedTimeUtc = DateTime.UtcNow;
        Count = count;
        TotalCost = totalCost;
        Status = status;
    }

    public static Order Create(Guid userId/*, int count, decimal totalCost = 0.0m, Status status = Status.Active*/)
    {
        var order = new Order(Guid.NewGuid(), userId);
        order.Validate();

        return order;
    }


    public void UpdateBooks(IEnumerable<Book> books)
    {
        EntityUpdater.UpdateRelatedEntities(
            _books,
            books,
            AddBookToOrder,
            RemoveBookFromOrder);
    }

    protected override void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfNegative(TotalCost, nameof(TotalCost));
    }

    public void CancelOrder()
    {
        Status = Status.Canceled;
        CanceledTimeUtc = DateTime.UtcNow;
    }
}
