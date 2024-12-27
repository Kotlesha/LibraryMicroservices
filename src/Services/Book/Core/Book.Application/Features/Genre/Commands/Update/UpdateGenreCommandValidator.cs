using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Genre.Commands.Update;

internal class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(g => g.GenreId)
            .NotEmpty();

        RuleFor(g => g.GenreDTO)
            .SetValidator(new GenreRequestDTOValidator());
    }
}