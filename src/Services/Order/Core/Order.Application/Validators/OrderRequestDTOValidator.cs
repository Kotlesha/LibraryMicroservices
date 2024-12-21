using FluentValidation;
using Order.Application.DTOs.RequestDTOs;

namespace Order.Application.Validators;

internal class OrderRequestDTOValidator : AbstractValidator<OrderRequestDTO>
{
    public OrderRequestDTOValidator()
    {
        RuleForEach(x => x.BooksIds)
            .NotEmpty();

        RuleFor(o => o.BooksIds)
            .NotEmpty();
    }
}
