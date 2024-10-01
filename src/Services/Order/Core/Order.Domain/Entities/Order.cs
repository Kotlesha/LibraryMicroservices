using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Order : AggregateRoot
{
    public Guid UserId { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private Order(Guid Id, Guid userId, decimal totalCost, DateTimeOffset createdDate) : base(Id)
    {
        UserId = userId;
        TotalCost = totalCost;
        CreatedDate = createdDate;
    }

    public static Order Create(Guid userId, decimal totalCost)
    {
        var order = new Order(Guid.NewGuid(), userId, totalCost, DateTimeOffset.UtcNow);
        order.Validate();

        return order;
    }

    public void Update(Order order) => TotalCost = order.TotalCost;

    public override void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfNegative(TotalCost, nameof(TotalCost));
    }
}
