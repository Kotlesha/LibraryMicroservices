using AutoMapper;
using Order.Application.DTOs.ResponseDTOs;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;

namespace Order.Application.Features.Order.Queries.GetByUserId;

internal class GetOrdersByUserIdQueryHandler(
    IOrderRepository orderRepository,
    IMapper mapper,
    IUserIdProvider userIdProvider) : IQueryHandler<GetOrdersByUserIdQuery,IEnumerable<OrderResponseDTO>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;

    public async Task<IEnumerable<OrderResponseDTO>>Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_userIdProvider.GetAuthUserId());
        var order = await _orderRepository.GetOrdersByUserIdAsync(userId, cancellationToken);
        return _mapper.Map<IEnumerable<OrderResponseDTO>>(order);
    }
}
