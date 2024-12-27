using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Queries.GetById;

public sealed record GetGenreByIdQuery(Guid GenreId) : IQuery<Result<GenreResponseDTO>>;