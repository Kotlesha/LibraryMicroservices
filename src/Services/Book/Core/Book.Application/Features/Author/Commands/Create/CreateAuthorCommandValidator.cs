using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Author.Commands.Create;

internal class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(a => a.AuthorDTO)
            .SetValidator(new AuthorRequestDTOValidator());
    }
}