using Book.Application.DTOs.ResponseDTOs;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Category.Queries.GetAll;

public sealed record GetAllCategoriesQuery : IQuery<IEnumerable<CategoryResponseDTO>>;