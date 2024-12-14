using Microsoft.AspNetCore.Http;
using Order.Application.DTOs.RequestDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Book.Commands.Update;

public sealed record UpdateBookCommand(
    Guid BookId,
    BookRequestDTO BookDTO) : ICommand<Result>;

