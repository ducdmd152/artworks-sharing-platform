using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubRepository.Repository;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class TransactionService : ITransactionService
{
    private readonly IDapperQueryService dapperQueryService;
    private readonly ITransactionRepository _itransactionRepository;
    private readonly ISystemConfigRepository systemConfigRepostory;

    public TransactionService(IDapperQueryService dapperQueryService, ITransactionRepository itransactionRepository, ISystemConfigRepository systemConfigRepository)
    {
        this.dapperQueryService = dapperQueryService;
        this._itransactionRepository = itransactionRepository;
        this.systemConfigRepostory = systemConfigRepository;
    }

    public async Task<StatisticOfWeekDto> GetStatisticOfRevenueLastWeek(string email)
    {
        return await dapperQueryService
        .SingleOrDefaultAsync<StatisticOfWeekDto>(QueryName.GetStatisticOfRevenueLastWeek, new { ArtistEmail = email });
    }

    public async Task<StatisticOfYearDto> GetStatisticOfRevenueMonthOfYear(string email)
    {
        return await dapperQueryService
        .SingleOrDefaultAsync<StatisticOfYearDto>(QueryName.GetStatisticOfRevenueMonthOfYear, new { ArtistEmail = email });
    }

    public IEnumerable<Transaction> GetTransactions()
    {
        return _itransactionRepository.GetTransactions();
    }

    public async Task<double> GetCommisionRate()
    {
        var systemConfig = await this.systemConfigRepostory.GetSystemConfig();
        return systemConfig.CommisionRate;
    }

    public double TotalRevenueForApp()
    {
        return _itransactionRepository.TotalRevenueForApp();
    }

	public async Task<StatisticOfYearDto> RevenueChartOfYear()
	{
		var statisticsOfYear = await dapperQueryService.SingleOrDefaultAsync<StatisticOfYearDto>(QueryName.RevenueChartOfYear, null);
		return statisticsOfYear;
	}

}