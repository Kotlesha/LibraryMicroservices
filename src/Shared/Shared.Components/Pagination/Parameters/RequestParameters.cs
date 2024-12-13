namespace Shared.Components.Pagination.Parameters;

public abstract class RequestParameters
{
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;

        set => _pageSize = value > RequestConstants.maxPageSize
            ? RequestConstants.maxPageSize : value;
    }
}
