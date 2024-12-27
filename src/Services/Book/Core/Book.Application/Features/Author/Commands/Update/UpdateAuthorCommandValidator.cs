using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Author.Commands.Update;

internal class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(a => a.AuthorId)
            .NotEmpty();

        RuleFor(a => a.AuthorDTO)
            .SetValidator(new AuthorRequestDTOValidator());
    }
}