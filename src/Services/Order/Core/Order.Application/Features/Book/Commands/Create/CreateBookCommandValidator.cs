using FluentValidation;
using Order.Application.DTOs.RequestDTOs;
using Order.Application.Validators;

namespace Order.Application.Features.Book.Commands.Create;

internal class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.BookDTO)
            .SetValidator(new BookRequestDTOValidator());
    }
}
