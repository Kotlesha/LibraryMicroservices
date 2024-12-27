using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Features.Author.Queries.GetBySurname;

internal class GetAuthorBySurnameQueryValidator : AbstractValidator<GetAuthorBySurnameQuery>
{
    public GetAuthorBySurnameQueryValidator()
    {
        RuleFor(a => a.AuthorSurname)
            .MaximumLength(AuthorConstants.SurnameMaxLength)
            .When(a => a.AuthorSurname is not null);
    }
}