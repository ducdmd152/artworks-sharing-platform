using ArtHubBO.DTO;

namespace ArtHubService.Interface;

public interface ISubscriberService
{
	public int GetTotalSubscribers();

	public Task<IEnumerable<subchart>> GetSubChaartQuery();
}
