using FluentValidation;

namespace Book.Application.Features.Author.Commands.Delete;

internal class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(a => a.AuthorId)
            .NotEmpty();
    }
}