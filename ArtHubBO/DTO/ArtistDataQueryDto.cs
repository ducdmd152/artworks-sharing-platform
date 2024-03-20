namespace ArtHubBO.DTO;

public class ArtistDataQueryDto
{
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public string? ArtistName { get; set; }
    public string? Bio { get; set; }
    public int TotalSubscriber { get; set; }
    public int TotalReact { get; set; }
    public int TotalView { get; set; }
    public int TotalBookmark { get; set; }
    public string? PostDetail { get; set; }
    public int TotalPostCount { get; set; }    
}
