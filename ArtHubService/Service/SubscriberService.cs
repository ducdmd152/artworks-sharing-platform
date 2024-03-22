using ArtHubBO.DTO;
using ArtHubRepository.DapperService;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class SubscriberService : ISubscriberService
{
	private readonly ISubscriberRepository _subcribersRepository;
	private readonly IDapperQueryService _dapperQueryService;

	public SubscriberService(ISubscriberRepository subcribersRepository, IDapperQueryService dapperQueryService)
	{
		_subcribersRepository = subcribersRepository;
		this._dapperQueryService = dapperQueryService;
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

			var result = _dapperQueryService.Select<Subchart>(queryName, queryParams);

			return result;
		}catch(Exception ex)
		{
			return null;
		}
	}

	public int GetTotalSubscribers()
	{

		return _subcribersRepository.GetTotalSubscribers();
	}
}
