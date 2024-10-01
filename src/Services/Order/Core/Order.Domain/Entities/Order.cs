using Shared.CleanArchitecture.Domain.Entities;
using System.Diagnostics;

namespace Order.Domain.Entities;

public sealed class Order : AggregateRoot
{

    public Guid UserId { get; private set; }

    public decimal TotalCost {  get; private set; }

    public DateTimeOffset CreatedDate { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();
    private Order(Guid userId, decimal totalCost, DateTimeOffset createdDate) : base(userId)
    {
        TotalCost = totalCost;
        this.CreatedDate = createdDate;
    }

    public static Order Create(decimal totalCost)
    {
        var order =new Order(Guid.NewGuid(), totalCost, DateTimeOffset.UtcNow);
        order.Validate();

        return order;
    }

    public void Update(Order order)
    {
        TotalCost = order.TotalCost;
    }

    public override void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfNegative(TotalCost, nameof(TotalCost));
    }
}
