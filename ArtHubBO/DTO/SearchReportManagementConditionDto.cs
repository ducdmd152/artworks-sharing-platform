using System.ComponentModel;

namespace ArtHubBO.DTO;

public class SearchReportManagementConditionDto
{
    public int Status { get; set; }
    public int PageNumber { get; set; }
    [DefaultValue(10)]
    public int PageSize { get; set; } 
}