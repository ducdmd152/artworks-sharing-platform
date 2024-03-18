using ArtHubBO.DTO;
using ArtHubBO.Enum;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IReportService
{
    PageResult<ReportManagementItem> GetReportList(SearchReportManagementConditionDto searchCondition);
    Task<Result> SkipOrBanPostAsync(int reportModeReportId, int reportModeMode);
}
