using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Queries.GetByName;

public sealed record GetGenreByNameQuery(string GenreName) : IQuery<Result<GenreResponseDTO>>;