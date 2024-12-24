using Order.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Order.Application.Features.Book.Queries.GetAll;

public sealed record GetAllBooksQuery : IQuery<IEnumerable<BookResponseDTO>>;
