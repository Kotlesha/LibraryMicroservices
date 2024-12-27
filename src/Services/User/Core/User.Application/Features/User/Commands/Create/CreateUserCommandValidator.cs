using FluentValidation;

namespace User.Application.Features.User.Commands.Create;

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty();

        RuleFor(u => u.Surname)
            .NotEmpty();

        RuleFor(u => u.Patronymic)
            .NotEmpty();

        RuleFor(u => u.Email)
            .EmailAddress();

        RuleFor(u => u.AccountId)
            .NotEmpty();
    }
}
