using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Commands.Create;

public sealed record CreateAuthorCommand(
    AuthorRequestDTO AuthorDTO) : ICommand<Result<Guid>>;