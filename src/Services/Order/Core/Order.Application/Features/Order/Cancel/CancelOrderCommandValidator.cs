using FluentValidation;
using Order.Application.Features.Order.Cancel;
using Order.Application.Validators;

namespace Order.Application.Features.Order.Commands.Cancel;

internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(c => c.OrderId)
            .NotEmpty();

        RuleFor(c => c.OrderDTO)
            .SetValidator(new OrderRequestDTOValidator());
    }
}
