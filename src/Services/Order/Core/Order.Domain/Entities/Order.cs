using Order.Domain.Enums;
using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Order : AggregateRoot
{
    public Guid UserId { get; private set; }
    public DateTime CreatedTimeUtc { get; private set; }
    public decimal TotalCost { get; private set; }
    public Status Status { get; private set; }
    public DateTime CanceledTimeUtc {  get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    public void AddBookToOrder(Book book) => AddEntity(book, _books);

    public void RemoveBookFromOrder(Book book) => RemoveEntity(book, _books); 

    private Order(Guid Id, Guid userId, decimal totalCost, Status status) : base(Id)
    {
        UserId = userId;
        CreatedTimeUtc = DateTime.UtcNow;
        TotalCost = totalCost;
        Status = status;
    }

    public static Order Create(Guid userId, decimal totalCost = 0.0m, Status status = Status.Active)
    {
        var order = new Order(Guid.NewGuid(), userId, totalCost, status);
        order.Validate();

        return order;
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
