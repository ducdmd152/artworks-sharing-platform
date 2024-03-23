using ArtHubBO.DTO;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class TransactionService : ITransactionService
{
    private readonly IDapperQueryService dapperQueryService;

    public TransactionService(IDapperQueryService dapperQueryService)
    {
        this.dapperQueryService = dapperQueryService;
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
}
