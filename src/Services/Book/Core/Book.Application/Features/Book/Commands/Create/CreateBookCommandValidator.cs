using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Book.Commands.Create;

internal class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.BookDTO)
            .SetValidator(new BookRequestDTOValidator());
    }
}