using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Commands.Update;

public sealed record UpdateAuthorCommand(Guid AuthorId, 
    AuthorRequestDTO AuthorDTO) : ICommand<Result>;