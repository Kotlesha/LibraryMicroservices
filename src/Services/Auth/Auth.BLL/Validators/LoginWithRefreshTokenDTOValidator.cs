using Auth.BLL.DTOs.RequestDTOs;
using FluentValidation;

namespace Auth.BLL.Validators;

internal class LoginWithRefreshTokenDTOValidator : AbstractValidator<LoginWithRefreshTokenDTO>
{
    public LoginWithRefreshTokenDTOValidator()
    {
        RuleFor(l => l.RefreshToken)
            .NotEmpty();
    }
}
