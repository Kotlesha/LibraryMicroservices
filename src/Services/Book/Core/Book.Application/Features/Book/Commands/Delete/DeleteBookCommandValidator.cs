using FluentValidation;

namespace Book.Application.Features.Book.Commands.Delete;

internal class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(c => c.BookId)
            .NotEmpty();
    }
}