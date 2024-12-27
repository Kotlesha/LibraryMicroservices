using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Book.Queries.GetAll;

public sealed record GetAllBooksQuery(Guid? AuthorId, Guid? GenreId) 
    : IQuery<IEnumerable<BookResponseDTO>>;