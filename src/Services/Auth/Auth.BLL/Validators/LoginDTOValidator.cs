using Auth.BLL.DTOs.RequestDTOs;
using FluentValidation;

namespace Auth.BLL.Validators;

internal class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(ld => ld.Email)
            .NotEmpty();

        RuleFor(ld => ld.Password)
            .NotEmpty();
    }
}
