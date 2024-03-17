namespace ArtHubBO.DTO;

public class PostManagementItem
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public DateTime Date { get; set; }
    public string Email { get; set; }
    public string ArtistName { get; set; }
    public int Scope { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    
}