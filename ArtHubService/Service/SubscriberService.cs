using ArtHubBO.DTO;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository subscriberRepository;
    private readonly IDapperQueryService dapperQueryService;
    private readonly IUnitOfWork unitOfWork;

    public SubscriberService(ISubscriberRepository subscriberRepository, IDapperQueryService dapperQueryService, IUnitOfWork unitOfWork = null)
    {
        this.subscriberRepository = subscriberRepository;
        this.dapperQueryService = dapperQueryService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<StatisticOfWeekDto> GetStatisticOfSubscriberLastWeek(string email)
    {
        return await dapperQueryService
                .SingleOrDefaultAsync<StatisticOfWeekDto>(QueryName.GetStatisticOfSubscriberLastWeek, new { ArtistEmail = email });
    }

    public async Task<StatisticOfYearDto> GetStatisticOfSubscriberMonthOfYear(string email)
    {
        return await dapperQueryService
               .SingleOrDefaultAsync<StatisticOfYearDto>(QueryName.GetStatisticOfSubscriberMonthOfYear, new { ArtistEmail = email });
    }
}
