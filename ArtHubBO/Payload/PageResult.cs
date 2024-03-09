namespace ArtHubBO.Payload;

public class PageResult<T>
{
    private PageInfo PageInfo { get; set; }
    private List<T> PageData { get; set; }

    public PageResult<T> PageNoData(PageInfo pageInfo)
    {
        PageResult<T> result = new PageResult<T>();
        result.PageInfo = new PageInfo(pageInfo);
        result.PageInfo.TotalPages = 0;
        result.PageInfo.TotalItems = 0;
        result.PageData = new List<T>();
        return result;
    }

    public PageResult<T> Build(PageInfo pageInfo, int totalItems, List<T> pageData)
    {
        PageResult<T> result = new PageResult<T>();
        result.PageInfo = new PageInfo(pageInfo.PageNum, pageInfo.PageSize, totalItems);
        result.PageData = pageData;
        return result;
    }
}
