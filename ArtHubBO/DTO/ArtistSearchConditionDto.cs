namespace ArtHubBO.DTO;
public class ArtistSearchConditionDto
{
    public bool IsGetDataPost { get; set; }
    public string Email { get; set; } = null!;
    public int[]? PostScope { get; set; }
    public int[]? PostStatus { get; set; }
    public bool IsOrderByReact { get; set; }
    public bool IsOrderByView { get; set; }
    public bool IsOrderByTitle { get; set; }
    public bool IsOrderAsc { get; set; }
    public int AccountStatus { get; set; }
    public bool AccountIsEnable { get; set; }
}
