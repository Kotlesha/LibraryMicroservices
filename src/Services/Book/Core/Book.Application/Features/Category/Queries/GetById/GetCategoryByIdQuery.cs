using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Queries.GetById;

public sealed record GetCategoryByIdQuery(Guid CategoryId) : IQuery<Result<CategoryResponseDTO>>;