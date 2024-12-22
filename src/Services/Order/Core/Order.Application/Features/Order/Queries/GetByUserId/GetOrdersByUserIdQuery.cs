using Order.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Order.Application.Features.Order.Queries.GetByUserId;

public sealed record GetOrdersByUserIdQuery : IQuery<IEnumerable<OrderResponseDTO>>;
