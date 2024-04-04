using ArtHubBO.Enum;

namespace ArtHubBO.DTO;

public class PostAudienceSearchConditionDto
{
    public string ArtistEmail { get; set; } = null!;
    public string OthersOfArtistEmail { get; set; } = null!;
    public string AudienceEmail { get; set; } = null!;
    public List<int> NotIncludePosts { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
    public string? Title { get; set; }
    public PostStatus? PostStatus { get; set; }
    public int[]? CategoryId { get; set; }
    public int[]? SuggestCategoryId { get; set; }
    public int? ReactFrom { get; set; }
    public int? ReactTo { get; set; }
    public int? BookmarkFrom { get; set; }
    public int? BookmarkTo { get; set;}
    public int? ViewFrom { get; set; }
    public int? ViewTo { get; set; }
    public SortType? SortType { get; set; } = Enum.SortType.FAVOURITE;
    public SortDirection? SortDirection { get; set; } = Enum.SortDirection.DESC;
}
