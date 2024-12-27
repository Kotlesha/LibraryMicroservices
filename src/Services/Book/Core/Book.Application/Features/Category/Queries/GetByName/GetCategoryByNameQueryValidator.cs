using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Features.Category.Queries.GetByName;

internal class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByNameQuery>
{
    public GetCategoryByNameQueryValidator()
    {
        RuleFor(c => c.CategoryName)
            .NotEmpty()
            .MaximumLength(CategoryConstants.NameMaxLength)
            .MinimumLength(CategoryConstants.NameMinLength);
    }
}