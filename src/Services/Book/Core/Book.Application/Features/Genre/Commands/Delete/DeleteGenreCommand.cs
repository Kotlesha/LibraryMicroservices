using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Commands.Delete;

public sealed record DeleteGenreCommand(
    Guid GenreId) : ICommand<Result>;