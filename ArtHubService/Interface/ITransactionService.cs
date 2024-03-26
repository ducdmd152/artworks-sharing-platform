using ArtHubBO.DTO;
using ArtHubBO.Entities;

namespace ArtHubService.Interface;

public interface ITransactionService
{
	public double TotalRevenueForApp();
    IEnumerable<Transaction> GetTransactions();
    Task<StatisticOfWeekDto> GetStatisticOfRevenueLastWeek(string email);
    Task<StatisticOfYearDto> GetStatisticOfRevenueMonthOfYear(string email);
}
