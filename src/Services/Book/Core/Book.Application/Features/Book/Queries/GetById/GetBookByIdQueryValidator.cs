using FluentValidation;

namespace Book.Application.Features.Book.Queries.GetById;

internal class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(b => b.BookId)
            .NotEmpty();
    }
}