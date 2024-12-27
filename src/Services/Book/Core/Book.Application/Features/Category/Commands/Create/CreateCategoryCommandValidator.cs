using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Category.Commands.Create;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryDTO)
           .SetValidator(new CategoryRequestDTOValidator());
    }
}