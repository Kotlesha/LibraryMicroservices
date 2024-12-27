using Book.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Book.Commands.Create;

public sealed record CreateBookCommand(
    BookRequestDTO BookDTO) : ICommand<Result<Guid>>;