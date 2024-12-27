using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Queries.GetById;

internal class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper) : IQueryHandler<GetCategoryByIdQuery, Result<CategoryResponseDTO>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CategoryResponseDTO>> Handle(
        GetCategoryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            return Result.Failure<CategoryResponseDTO>(ApplicationErrors.Category.NotFound);
        }

        var resultCategory = _mapper.Map<CategoryResponseDTO>(category);

        return resultCategory;
    }
}
