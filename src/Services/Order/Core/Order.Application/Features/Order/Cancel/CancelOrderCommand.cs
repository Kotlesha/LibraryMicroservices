using Order.Application.DTOs.RequestDTOs;
using Order.Domain.Enums;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Cancel;

public sealed record CancelOrderCommand(
    Guid OrderId,
    OrderRequestDTO OrderDTO) : ICommand<Result>;
