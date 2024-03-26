using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class TransactionService : ITransactionService
{
    private readonly IDapperQueryService dapperQueryService;
    private readonly ITransactionRepository _itransactionRepository;

    public TransactionService(IDapperQueryService dapperQueryService, ITransactionRepository itransactionRepository)
    {
        this.dapperQueryService = dapperQueryService;
         this._itransactionRepository = itransactionRepository;
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

    public double TotalRevenueForApp()
    {
        return _itransactionRepository.TotalRevenueForApp(); 
    }
}
