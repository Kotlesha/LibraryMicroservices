using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Features.Book.Queries.GetByTitle;

internal class GetBookByTitleQueryValidator : AbstractValidator<GetBookByTitleQuery>
{
    public GetBookByTitleQueryValidator()
    {
        RuleFor(b => b.BookTitle)
            .NotEmpty()
            .MaximumLength(BookConstants.TitleMaxLength)
            .MinimumLength(BookConstants.TitleMinLength);
    }
}