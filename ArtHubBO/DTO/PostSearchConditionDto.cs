using ArtHubBO.Enum;

namespace ArtHubBO.DTO;

public class PostSearchConditionDto
{
    public string ArtistEmail { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
    public string? Title { get; set; }
    public PostStatus? PostStatus { get; set; }
    public PostScope? PostScope { get; set; }
    public int[]? CategoryId { get; set; }
    public int? ReactFrom { get; set; }
    public int? ReactTo { get; set; }
    public int? BookmarkFrom { get; set; }
    public int? BookmarkTo { get; set;}
    public int? ViewFrom { get; set; }
    public int? ViewTo { get; set; }   
    public SortDirection? SortDirection { get; set; }
}
