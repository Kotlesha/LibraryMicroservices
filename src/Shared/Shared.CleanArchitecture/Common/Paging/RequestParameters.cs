namespace Shared.CleanArchitecture.Common.Paging;

public abstract class RequestParameters
{
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    private const int _maxPageSize = 50;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
    }
}
