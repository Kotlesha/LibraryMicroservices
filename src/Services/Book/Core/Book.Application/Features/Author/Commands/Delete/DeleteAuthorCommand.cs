using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Commands.Delete;

public sealed record DeleteAuthorCommand(
    Guid AuthorId) : ICommand<Result>;