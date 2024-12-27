using Book.Application.DTOs.RequestDTOs;
using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Validators;

internal class CategoryRequestDTOValidator : AbstractValidator<CategoryRequestDTO>
{
    public CategoryRequestDTOValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(CategoryConstants.NameMaxLength)
            .MinimumLength(CategoryConstants.NameMinLength);
    }
}