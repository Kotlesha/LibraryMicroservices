using FluentValidation;
using Order.Application.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
