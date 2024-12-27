using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Book.Commands.Update;

internal class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(c => c.BookId)
            .NotEmpty();

        RuleFor(c => c.BookDTO)
            .SetValidator(new BookRequestDTOValidator());
    }
}