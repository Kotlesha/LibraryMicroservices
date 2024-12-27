using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Category.Queries.GetAll;

internal class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponseDTO>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CategoryResponseDTO>> Handle(
        GetAllCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
    }
}