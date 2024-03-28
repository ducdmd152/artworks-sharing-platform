using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface ISubscriberRepository : IBaseRepository<Subscriber> 
    {
        List<string> GetSubscribingArtistEmailList(string audienceEmail);
        public int GetTotalSubscribersWithinLast30Days();

	}
}
