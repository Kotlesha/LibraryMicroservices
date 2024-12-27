using Order.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Commands.Update;

public sealed record UpdateBookCommand(
    Guid BookId,
    BookRequestDTO BookDTO) : ICommand<Result>;

