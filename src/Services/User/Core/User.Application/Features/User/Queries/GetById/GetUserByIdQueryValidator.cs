using FluentValidation;

namespace User.Application.Features.User.Queries.GetById;

internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(u => u.ApplicationUserId)
            .NotEmpty();
    }
}
