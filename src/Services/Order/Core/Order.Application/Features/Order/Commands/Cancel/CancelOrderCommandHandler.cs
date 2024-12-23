using Order.Application.Errors;
using Order.Domain.Enums;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Cancel;

internal class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    IUserIdProvider userIdProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(ApplicationErrors.Order.NotFound);
        }

        var userId = Guid.Parse(_userIdProvider.GetAuthUserId());

        if (order.UserId != userId)
        {
            return Result.Failure(ApplicationErrors.Order.NotBelongToUser);
        }

        if (order.Status == Status.Cancelled)
        {
            return Result.Failure(ApplicationErrors.Order.AlreadyCancelled);
        }

        order.CancelOrder();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}


