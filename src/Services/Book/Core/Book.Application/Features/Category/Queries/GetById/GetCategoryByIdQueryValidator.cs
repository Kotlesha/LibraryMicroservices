using FluentValidation;

namespace Book.Application.Features.Category.Queries.GetById;

internal class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty();
    }
}