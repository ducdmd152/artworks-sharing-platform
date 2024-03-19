using ArtHubBO.DTO;
using ArtHubRepository.DapperService;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class SubscriberService : ISubscriberService
{
	private readonly ISubscriberRepository _subcribersRepository;
	private readonly DapperQueryService _dapperQueryService;

	public SubscriberService(ISubscriberRepository subcribersRepository, DapperQueryService dapperQueryService)
	{
		_subcribersRepository = subcribersRepository;
		_dapperQueryService = dapperQueryService;
	}

	public async Task<IEnumerable<subchart>> GetSubChaartQuery()
	{
		var queryName = QueryName.SubcribeChartQuery;
		var queryParams = new
		{
			StartDate = "2024-05-05",
			EndDate = "2024-06-06"
		};

		var result = await _dapperQueryService.SelectAsync<subchart>(queryName, queryParams);

		return result.ToList();
	}

	public int GetTotalSubscribers()
	{

		return _subcribersRepository.GetTotalSubscribers();
	}
}
