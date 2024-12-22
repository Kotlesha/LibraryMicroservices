using Order.Application.Errors;
using Order.Application.Services;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Update;

internal class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderService orderService,
    IUnitOfWork unitOfWork,
    IUserIdProvider userIdProvider) : ICommandHandler<UpdateOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderService _orderService = orderService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        var userId = Guid.Parse(_userIdProvider.GetAuthUserId());

        if (order is null)
        {
            return Result.Failure(ApplicationErrors.Order.NotFound);
        }

        if (order.UserId != userId)
        {
            return Result.Failure(ApplicationErrors.Order.NotBelongToUser);
        }

        var result = await _orderService.ValidateAndRetrieveBooksAsync(request.OrderDTO.BooksIds, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        order.UpdateBooks(result.Value);

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
