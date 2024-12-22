using FluentValidation;
using Order.Application.Validators;

namespace Order.Application.Features.Order.Commands.Cancel;

internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(c => c.OrderId)
            .NotEmpty();
    }
}
