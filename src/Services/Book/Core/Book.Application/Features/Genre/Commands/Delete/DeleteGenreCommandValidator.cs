using FluentValidation;

namespace Book.Application.Features.Genre.Commands.Delete;

internal class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(g => g.GenreId)
            .NotEmpty();
    }
}