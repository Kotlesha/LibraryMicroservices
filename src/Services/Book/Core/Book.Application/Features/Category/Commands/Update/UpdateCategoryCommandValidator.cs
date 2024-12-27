using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Category.Commands.Update;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty();

        RuleFor(c => c.CategoryDTO)
            .SetValidator(new CategoryRequestDTOValidator());
    }
}