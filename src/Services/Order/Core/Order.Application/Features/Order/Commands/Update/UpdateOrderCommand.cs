using Order.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Update;

public sealed record UpdateOrderCommand(
    Guid OrderId,
    OrderRequestDTO OrderDTO) : ICommand<Result>;
