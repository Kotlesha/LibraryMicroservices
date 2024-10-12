using FluentValidation;
using User.Domain.Constants;

namespace User.Application.Features.User.Commands.Create;

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .MaximumLength(UserConstants.NameMaxLength);

        RuleFor(u => u.Surname)
            .NotEmpty()
            .MaximumLength(UserConstants.SurnameMaxLength);

        RuleFor(u => u.Patronymic)
            .NotEmpty()
            .MaximumLength(UserConstants.PatronymicMaxLength);

        RuleFor(u => u.Email)
            .EmailAddress();

        RuleFor(u => u.ApplicationUserId)
            .NotEmpty();
    }
}
