namespace ArtHubBO.DTO;

public class PostDetailDto
{
	public int PostId { get; set; }
	public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Status { get; set; }
    public int Scope { get; set; }
    public int TotalReact { get; set; }
    public int TotalView { get; set; }
    public int TotalBookmark { get; set; }
    public string ArtistEmail { get; set; } = null!;
	public string ImageUrls { get; set; } = null!;
}