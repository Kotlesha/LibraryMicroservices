using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Order.Commands.Cancel;

public sealed record CancelOrderCommand(
    Guid OrderId) : ICommand<Result>;
