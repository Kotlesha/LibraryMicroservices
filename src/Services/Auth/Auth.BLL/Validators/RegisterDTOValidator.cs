﻿using Auth.BLL.Constants.Passwords;
using Auth.BLL.DTOs.RequestDTOs;
using Auth.DAL.Constants;
using FluentValidation;

namespace Auth.BLL.Validators;

internal class RegisterDTOValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDTOValidator()
    {
        RuleFor(rd => rd.Name)
            .NotEmpty();

        RuleFor(rd => rd.Surname)
            .NotEmpty();

        RuleFor(rd => rd.Patronymic)
            .NotEmpty();

        RuleFor(rd => rd.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(AccountConstants.EmailMaxLength);

        RuleFor(rd => rd.Password)
            .NotEmpty();

        RuleFor(rd => rd.Password)
            .Length(PasswordRestrictions.MinimumLength, PasswordRestrictions.MaximumLength)
            .Matches(PasswordRestrictions.UppercaseLetter)
                .WithMessage(PasswordErrorMessages.UppercaseLetter)
            .Matches(PasswordRestrictions.LowercaseLetter)
                .WithMessage(PasswordErrorMessages.LowercaseLetter)
            .Matches(PasswordRestrictions.Digit)
                .WithMessage(PasswordErrorMessages.Digit)
            .Matches(PasswordRestrictions.SpecialCharacter)
                .WithMessage(PasswordErrorMessages.SpecialCharacter);

        RuleFor(rd => rd.PasswordConfirmation)
            .Equal(rd => rd.Password)
            .NotEmpty();
    }
}
