using FluentValidation;

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
