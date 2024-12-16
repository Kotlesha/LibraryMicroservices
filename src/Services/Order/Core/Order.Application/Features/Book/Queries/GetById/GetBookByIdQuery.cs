using Order.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Queries.GetById;

public sealed record GetBookByIdQuery(Guid BookId) : IQuery<Result<BookResponseDTO>>;

