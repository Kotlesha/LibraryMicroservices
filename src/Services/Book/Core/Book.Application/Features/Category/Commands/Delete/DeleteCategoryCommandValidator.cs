using FluentValidation;

namespace Book.Application.Features.Category.Commands.Delete;

internal class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty();
    }
}