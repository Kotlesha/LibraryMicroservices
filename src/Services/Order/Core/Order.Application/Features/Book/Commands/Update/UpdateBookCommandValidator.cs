using FluentValidation;
using Order.Application.Validators;

namespace Order.Application.Features.Book.Commands.Update;

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
    
