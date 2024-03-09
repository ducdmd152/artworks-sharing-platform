namespace ArtHubBO.Payload;

public class SearchPayload<T>
{
    public PageInfo PageInfo { get; set; }
    public T SearchCondition { get; set; }

    public SearchPayload(PageInfo pageInfo, T searchCondition)
    {
        PageInfo = pageInfo;
        SearchCondition = searchCondition;
    }
}
