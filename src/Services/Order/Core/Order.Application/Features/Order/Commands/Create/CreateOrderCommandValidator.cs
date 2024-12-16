using FluentValidation;
using Order.Application.Validators;

namespace Order.Application.Features.Order.Commands.Create;

internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.OrderDTO)
            .SetValidator(new OrderRequestDTOValidator());
    }
}
