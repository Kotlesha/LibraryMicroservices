using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Book.Commands.Delete;

public sealed record DeleteBookCommand(
    Guid BookId) : ICommand<Result>;