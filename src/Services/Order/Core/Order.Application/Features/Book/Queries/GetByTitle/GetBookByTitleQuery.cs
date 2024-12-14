using Order.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;

namespace Order.Application.Features.Book.Queries.GetByTitle;

public sealed record GetBookByTitleQuery(string title) : IQuery<Result<BookResponseDTO>>;


