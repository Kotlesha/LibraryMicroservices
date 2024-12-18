using FluentValidation;
using Order.Application.Features.Order.Commands.Create;

namespace Order.Application.Features.Order.Commands.Delete
{
    internal class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(c => c.OrderId)
                .NotEmpty();
        }
    }
}
