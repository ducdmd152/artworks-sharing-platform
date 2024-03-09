namespace ArtHubBO.Payload;

public class PageInfo
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    public PageInfo(int pageNum, int pageSize)
    {
        PageNum = pageNum;
        PageSize = pageSize;
    }

    public PageInfo(int pageNum, int pageSize, int totalItems)
        : this(pageNum, pageSize)
    {
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
    }

    public PageInfo(PageInfo pageInfo)
    {
        if (pageInfo != null)
        {
            PageNum = pageInfo.PageNum;
            PageSize = pageInfo.PageSize;
            TotalItems = pageInfo.TotalItems;
            TotalPages = pageInfo.TotalPages;
        }
    }
}
