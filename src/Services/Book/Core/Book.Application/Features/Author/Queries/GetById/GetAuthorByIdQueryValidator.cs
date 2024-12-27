using FluentValidation;

namespace Book.Application.Features.Author.Queries.GetById;

internal class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(a => a.AuthorId)
            .NotEmpty();
    }
}