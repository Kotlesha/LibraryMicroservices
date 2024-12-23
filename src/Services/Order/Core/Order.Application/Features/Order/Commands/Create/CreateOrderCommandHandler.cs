using Order.Application.Abstractions.Services;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Create;

using Order = Domain.Entities.Order;

internal class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderService orderService,
    IUnitOfWork unitOfWork,
    IUserIdProvider userIdProvider) : ICommandHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderService _orderService = orderService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_userIdProvider.GetAuthUserId());
        var order = Order.Create(userId);

        var result = await _orderService.ValidateAndRetrieveBooksAsync(request.OrderDTO.BooksIds, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        foreach (var book in result.Value)
        {
            order.AddBookToOrder(book);
        }

        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}

