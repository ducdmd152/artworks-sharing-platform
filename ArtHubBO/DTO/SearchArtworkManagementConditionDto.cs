using System.ComponentModel;

namespace ArtHubBO.DTO;

public class SearchArtworkManagementConditionDto
{
    public string ArtworkTitle { get; set; }
    public int ?ArtworkID { get; set; }
    public DateTime ?Date { get; set; }
    public string ArtworkName { get; set; }
    public string Status { get; set; }
    public int PageNumber { get; set; }
    [DefaultValue(10)]
    public int PageSize { get; set; } 
}