using FluentValidation;

namespace Book.Application.Features.Genre.Queries.GetById;

internal class GetGenreByIdQueryValidator : AbstractValidator<GetGenreByIdQuery>
{
    public GetGenreByIdQueryValidator()
    {
        RuleFor(g => g.GenreId)
            .NotEmpty();
    }
}