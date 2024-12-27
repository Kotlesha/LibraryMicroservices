using AutoMapper;
using Book.Application.Abstractions.Service;
using Book.Application.Errors;
using Book.Application.Extensions;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Category.Commands.Create;

using Category = Domain.Entities.Category;

internal class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryNameAlreadyExists = await _categoryRepository.GetCategoryByNameAsync(
            request.CategoryDTO.Name,
            cancellationToken);

        if (categoryNameAlreadyExists is not null)
        {
            return Result.Failure<Guid>(ApplicationErrors.Category.NameAlreadyExists);
        }

        var category = _mapper.Map<Category>(request.CategoryDTO);

        _categoryRepository.Add(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}