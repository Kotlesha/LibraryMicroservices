using Book.Application.DTOs.RequestDTOs;
using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Validators;

internal class AuthorRequestDTOValidator : AbstractValidator<AuthorRequestDTO>
{
    public AuthorRequestDTOValidator()
    {
        RuleFor(a => a.Surname)
            .NotEmpty()
            .MaximumLength(AuthorConstants.SurnameMaxLength)
            .When(a => a.Surname is not null);

        RuleFor(a => a.Name)
            .NotEmpty()
            .MaximumLength(AuthorConstants.NameMaxLength)
            .MinimumLength(AuthorConstants.NameMinLength);
    }
}