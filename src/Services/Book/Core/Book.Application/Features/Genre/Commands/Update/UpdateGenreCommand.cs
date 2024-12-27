using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Commands.Update;

public sealed record UpdateGenreCommand(Guid GenreId,
    GenreRequestDTO GenreDTO) : ICommand<Result>;