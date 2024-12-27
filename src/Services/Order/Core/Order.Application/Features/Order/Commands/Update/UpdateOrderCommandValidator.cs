using FluentValidation;
using Order.Application.Validators;

namespace Order.Application.Features.Order.Commands.Update;

internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.OrderId)
            .NotEmpty();

        RuleFor(c => c.OrderDTO)
            .SetValidator(new OrderRequestDTOValidator());
    }
}
