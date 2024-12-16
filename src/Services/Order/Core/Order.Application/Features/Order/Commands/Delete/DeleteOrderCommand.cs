using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Delete;

public sealed record DeleteOrderCommand(
    Guid OrderId) : ICommand<Result>;

