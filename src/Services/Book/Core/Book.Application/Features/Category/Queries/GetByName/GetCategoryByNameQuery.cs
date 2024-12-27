using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Queries.GetByName;

public sealed record GetCategoryByNameQuery(string CategoryName) : IQuery<Result<CategoryResponseDTO>>;