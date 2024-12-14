using Order.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Order.Commands.Create;

public sealed record CreateOrderCommand(
    OrderRequestDTO OrderDTO) : ICommand<Result<Guid>>;
     