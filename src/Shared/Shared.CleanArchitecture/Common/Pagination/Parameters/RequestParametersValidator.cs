using FluentValidation;

namespace Shared.CleanArchitecture.Common.Pagination.Parameters;

public class RequestParametersValidator : AbstractValidator<RequestParameters>
{
    public RequestParametersValidator()
    {
        RuleFor(rp => rp.PageSize)
            .GreaterThanOrEqualTo(RequestConstants.minPageSize)
            .LessThanOrEqualTo(RequestConstants.maxPageSize);

        RuleFor(rp => rp.PageNumber)
            .GreaterThanOrEqualTo(RequestConstants.minPageNumber);
    }
}
