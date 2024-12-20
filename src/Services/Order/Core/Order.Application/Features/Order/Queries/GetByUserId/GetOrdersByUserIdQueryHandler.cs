using AutoMapper;
using Order.Application.DTOs.ResponseDTOs;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Queries.GetByUserId;

internal class GetOrdersByUserIdQueryHandler(
    IOrderRepository orderRepository,
    IMapper mapper) : IQueryHandler<GetOrdersByUserIdQuery, Result<OrderResponseDTO>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<OrderResponseDTO>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrdersByUserIdAsync(request.UserId, cancellationToken);

        if (order is null) 
        {
            return Result.Failure<OrderResponseDTO>(ApplicationErrors.Order.NotFound);
        }

        var resultOrder = _mapper.Map<OrderResponseDTO>(order);
        return resultOrder;
    }
}
