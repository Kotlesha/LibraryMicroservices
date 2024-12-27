using Auth.BLL.DTOs.RequestDTOs;
using Auth.DAL.Constants;
using FluentValidation;

namespace Auth.BLL.Validators;

internal class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(ld => ld.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(AccountConstants.EmailMaxLength);

        RuleFor(ld => ld.Password)
            .NotEmpty();
    }
}
