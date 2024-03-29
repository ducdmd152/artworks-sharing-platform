using ArtHubBO.DTO;
using ArtHubRepository.DapperService;
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

	public async Task<IEnumerable<Subchart>> GetSubChaartQuery()
	{
		try
		{
			var queryName = QueryName.SubcribeChartQuery;
			var queryParams = new
			{
				StartDate = DateTime.Parse("2024-03-18"),
				EndDate = DateTime.Parse("2024-03-20")
			};

			var result = dapperQueryService.Select<Subchart>(queryName, queryParams);

			return result;
		}catch(Exception ex)
		{
			return null;
		}
	}

	public int GetTotalSubscribers()
	{

		return subscriberRepository.GetTotalSubscribersWithinLast30Days();
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

	public async Task<StatisticOfWeekDto> GetSubChartOneWeek()
	{
		var statisticsOfYear = await dapperQueryService.SingleOrDefaultAsync<StatisticOfWeekDto>(QueryName.SubChartQuery, null);
		return statisticsOfYear;
	}
}
