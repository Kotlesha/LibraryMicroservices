using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Book.Commands.Delete;

public sealed record DeleteBookCommand(
    Guid BookId) : ICommand<Result>;
