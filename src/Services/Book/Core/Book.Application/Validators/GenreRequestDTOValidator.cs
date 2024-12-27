using Book.Application.DTOs.RequestDTOs;
using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Validators;

internal class GenreRequestDTOValidator : AbstractValidator<GenreRequestDTO>
{
    public GenreRequestDTOValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .MaximumLength(GenreConstants.NameMaxLength)
            .MinimumLength(GenreConstants.NameMinLength);
    }
}