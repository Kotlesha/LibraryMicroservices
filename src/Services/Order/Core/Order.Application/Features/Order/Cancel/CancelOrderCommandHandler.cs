using Order.Application.Errors;
using Order.Application.Features.Order.Cancel;
using Order.Domain.Enums;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Cancel;

internal class CancelOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(ApplicationErrors.Order.NotFound);
        }

        if (order.UserId != request.OrderId)
        {
            return Result.Failure(ApplicationErrors.Order.NotBelongToUser);
        }

        if (order.Status == Status.Canceled)
        {
            return Result.Failure(ApplicationErrors.Order.AlreadyCancelled);
        }

        order.CancelOrder();


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}


