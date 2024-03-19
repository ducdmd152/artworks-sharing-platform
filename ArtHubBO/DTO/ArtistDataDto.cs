namespace ArtHubBO.DTO;

public class ArtistDataDto
{
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public string? ArtistName { get; set; }
    public string? Bio { get; set; }
    public int TotalSubscriber { get; set; }
    public int TotalReact { get; set; }
    public int TotalView { get; set; }
    public int TotalBookmark { get; set; }
    public List<PostDetailDto> PostDetailDtos { get; set; } = new List<PostDetailDto>();
}
