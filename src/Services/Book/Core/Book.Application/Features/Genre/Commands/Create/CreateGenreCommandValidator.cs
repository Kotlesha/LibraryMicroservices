using Book.Application.Validators;
using FluentValidation;

namespace Book.Application.Features.Genre.Commands.Create;

internal class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(g => g.GenreDTO)
           .SetValidator(new GenreRequestDTOValidator());
    }
}