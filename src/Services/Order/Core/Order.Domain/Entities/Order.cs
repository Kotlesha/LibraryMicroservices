using Shared.CleanArchitecture.Domain.Entities;

namespace Order.Domain.Entities;

public sealed class Order : AggregateRoot
{
    public Guid UserId { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public decimal TotalCost => _books.Sum(b => b.Price);

    private readonly List<Book> _books = [];
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private Order(Guid Id, Guid userId) : base(Id)
    {
        UserId = userId;
        CreatedDate = DateTimeOffset.UtcNow;
    }

    public static Order Create(Guid userId)
    {
        var order = new Order(Guid.NewGuid(), userId);
        order.Validate();

        return order;
    }

    public override void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfNegative(TotalCost, nameof(TotalCost));
    }
}
