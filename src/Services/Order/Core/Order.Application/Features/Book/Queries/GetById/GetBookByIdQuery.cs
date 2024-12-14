using Order.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Book.Queries.GetById;

public sealed record GetBookByIdQuery(Guid BookId) : IQuery<Result<BookResponseDTO>>;

