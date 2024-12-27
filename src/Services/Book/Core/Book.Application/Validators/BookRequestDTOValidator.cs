using Book.Application.DTOs.RequestDTOs;
using Book.Domain.Constants;
using FluentValidation;

namespace Book.Application.Validators;

internal class BookRequestDTOValidator : AbstractValidator<BookRequestDTO>
{
    public BookRequestDTOValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .MaximumLength(BookConstants.TitleMaxLength)
            .MinimumLength(BookConstants.TitleMinLength);

        RuleFor(b => b.Description)
            .MaximumLength(BookConstants.DescriptionMaxLength)
            .When(b => b.Description is not null);

        RuleFor(b => b.Price)
            .GreaterThanOrEqualTo(decimal.Zero);

        RuleFor(b => b.Pages)
            .GreaterThan((short)0);

        RuleFor(b => b.AgeRating)
            .IsInEnum();

        RuleFor(b => b.ISBN)
            .Must(CheckValidityOfIsbn);

        RuleFor(b => b.CategoryId)
            .NotEmpty();

        RuleFor(b => b.AuthorsIds)
            .NotEmpty();

        RuleFor(b => b.GenresIds)
            .NotEmpty();

        RuleForEach(b => b.AuthorsIds)
            .NotEmpty();

        RuleForEach(b => b.GenresIds)
            .NotEmpty();
    }

    private bool CheckValidityOfIsbn(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        isbn = isbn.Replace("-", "").Replace(" ", "");

        if (isbn.Length != 13 || !isbn.All(char.IsDigit))
            return false;

        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = isbn[i] - '0';
            sum += i % 2 == 0 ? digit : digit * 3;
        }

        int checkDigit = 10 - (sum % 10);
        if (checkDigit == 10)
            checkDigit = 0;

        return checkDigit == (isbn[12] - '0');
    }
}