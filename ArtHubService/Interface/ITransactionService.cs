using ArtHubBO.DTO;

namespace ArtHubService.Interface;

public interface ITransactionService
{
    Task<StatisticOfWeekDto> GetStatisticOfRevenueLastWeek(string email);
    Task<StatisticOfYearDto> GetStatisticOfRevenueMonthOfYear(string email);
}
