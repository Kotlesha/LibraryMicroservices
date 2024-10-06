using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Order : AggregateRoot
{
    public Guid UserId { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public decimal TotalCost {  get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private Order(Guid Id, Guid userId, decimal totalCost) : base(Id)
    {
        UserId = userId;
        CreatedDate = DateTimeOffset.UtcNow;
        TotalCost = 0.0m;
    }

    public static Order Create(Guid userId, decimal totalCost)
    {
        var order = new Order(Guid.NewGuid(), userId, totalCost);
        order.Validate();

        return order;
    }

    public override void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfNegative(TotalCost, nameof(TotalCost));
    }

    public Result AddBookToOrder(Book book)
    {

    }
}
