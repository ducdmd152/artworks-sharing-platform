using ArtHubBO.DTO;

namespace ArtHubService.Interface;

public interface ISubscriberService
{
    Task<StatisticOfWeekDto> GetStatisticOfSubscriberLastWeek(string email);
    Task<StatisticOfYearDto> GetStatisticOfSubscriberMonthOfYear(string email);
}
