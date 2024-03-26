using ArtHubBO.DTO;

namespace ArtHubService.Interface;

public interface ISubscriberService
{
	public int GetTotalSubscribers();

	public Task<IEnumerable<Subchart>> GetSubChaartQuery();
    Task<StatisticOfWeekDto> GetStatisticOfSubscriberLastWeek(string email);
    Task<StatisticOfYearDto> GetStatisticOfSubscriberMonthOfYear(string email);
}
