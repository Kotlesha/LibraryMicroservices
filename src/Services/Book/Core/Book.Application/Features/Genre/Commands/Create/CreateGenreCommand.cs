using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Commands.Create;

public sealed record CreateGenreCommand(
    GenreRequestDTO GenreDTO) : ICommand<Result<Guid>>;