using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Genre.Queries.GetAll;

public sealed record GetAllGenresQuery : IQuery<IEnumerable<GenreResponseDTO>>;