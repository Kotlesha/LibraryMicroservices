using FluentValidation;
using Shared.Components.Pagination.Parameters;

namespace User.Application.Features.User.Queries.GetAll;

internal class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
        RuleFor(q => q.Parameters)
            .SetValidator(new RequestParametersValidator());
    }
}
