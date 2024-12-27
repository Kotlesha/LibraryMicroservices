using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Features.Genre.Queries.GetByName;

internal class GetGenreByNameQueryValidator : AbstractValidator<GetGenreByNameQuery>
{
    public GetGenreByNameQueryValidator()
    {
        RuleFor(g => g.GenreName)
            .NotEmpty()
            .MaximumLength(GenreConstants.NameMaxLength)
            .MinimumLength(GenreConstants.NameMinLength);
    }
}