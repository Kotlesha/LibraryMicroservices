using AutoMapper;
using Book.Application.Abstractions.Service;
using Book.Application.Errors;
using Book.Application.Extensions;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Commands.Update;

using Category = Domain.Entities.Category;

internal class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IGenreService genreService,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IGenreService _genreService = genreService;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            return Result.Failure(ApplicationErrors.Category.NotFound);
        }

        var newCategory = _mapper.Map<Category>(request.CategoryDTO);
        category.Update(newCategory);

        _categoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}