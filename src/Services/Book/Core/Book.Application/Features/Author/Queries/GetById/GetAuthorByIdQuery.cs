using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Queries.GetById;

public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<Result<AuthorResponseDTO>>;