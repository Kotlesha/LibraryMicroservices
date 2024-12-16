using Microsoft.AspNetCore.Identity;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Delete;

internal class DeleteOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null) 
        {
            return Result.Failure(ApplicationErrors.Order.NotFound);
        }

        if (order.Equals(order.UserId))
        {
            return Result.Failure(ApplicationErrors.Order.NotBelongToUser);
        }

        await _orderRepository.RemoveAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
