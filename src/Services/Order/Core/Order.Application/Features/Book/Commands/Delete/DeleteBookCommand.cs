using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Commands.Delete;

public sealed record DeleteBookCommand(
    Guid BookId) : ICommand<Result>;
