using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Author.Queries.GetAll;

public sealed record GetAllAuthorsQuery : IQuery<IEnumerable<AuthorResponseDTO>>;