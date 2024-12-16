using FluentValidation;
using Order.Application.DTOs.RequestDTOs;

namespace Order.Application.Validators;

internal class BookRequestDTOValidator : AbstractValidator<BookRequestDTO>
{
    public BookRequestDTOValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty();

        RuleFor(b => b.Price)
            .GreaterThanOrEqualTo(decimal.Zero);
    }
}
