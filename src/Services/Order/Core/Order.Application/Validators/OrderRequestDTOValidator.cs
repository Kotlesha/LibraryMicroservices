using FluentValidation;
using Order.Application.DTOs.RequestDTOs;

namespace Order.Application.Validators;

internal class OrderRequestDTOValidator : AbstractValidator<OrderRequestDTO>
{
    public OrderRequestDTOValidator()
    {
        RuleFor(o => o.BooksIds)
            .NotEmpty();

        RuleForEach(o => o.BooksIds)
            .NotEmpty();
    }
}
