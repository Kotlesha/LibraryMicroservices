using Order.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Queries.GetByTitle;

public sealed record GetBookByTitleQuery(string Title) : IQuery<Result<BookResponseDTO>>;