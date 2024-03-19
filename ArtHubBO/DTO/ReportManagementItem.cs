namespace ArtHubBO.DTO;

public class ReportManagementItem
{
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int ReportId { get; set; }
    public string Reason { get; set; }
    public string ReporterEmail { get; set; }
    public string FullName { get; set; }
    public int PostId { get; set; }
    public string ArtistEmail { get; set; }
    public string ArtistName { get; set; }
    public int Status { get; set; }
}