using Order.Domain.Enums;
using Order.Domain.Errors;
using Shared.CleanArchitecture.Common;
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

    private bool HasBook(Book book) => _books.Any(b => b.Id.Equals(book.Id));

    public Result AddBookToOrder(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        if (!book.IsAvailable)
        {
            return Result.Failure(DomainErrors.Order.BookNotAvailable);
        }

        if (HasBook(book))
        {
            return Result.Failure(DomainErrors.Order.BookAlreadyExists);
        }

        _books.Add(book);
        TotalCost += book.Price;

        return Result.Success();
    }

    public Result RemoveBookFromOrder(Book book)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        if (!HasBook(book))
        {
            return Result.Failure(DomainErrors.Order.BookNotFound);
        }

        _books.Remove(book);
        TotalCost -= book.Price;

        return Result.Success();
    }

    public void CancelOrder()
    {
        Status = Status.Canceled;
        CanceledTimeUtc = DateTime.UtcNow;
    }
}
