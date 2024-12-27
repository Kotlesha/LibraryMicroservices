using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Queries.GetBySurname;

public sealed record GetAuthorBySurnameQuery(string AuthorSurname) : IQuery<Result<IEnumerable<AuthorResponseDTO>>>;