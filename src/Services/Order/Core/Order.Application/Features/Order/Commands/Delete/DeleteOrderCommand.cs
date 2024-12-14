using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Order.Commands.Delete;

public sealed record DeleteOrderCommand(
    Guid OrderId) : ICommand<Result>;

