using Order.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Book.Commands.Create
{
    public sealed record CreateBookCommand(
        BookRequestDTO BookDTO) : ICommand<Result<Guid>>;
}
