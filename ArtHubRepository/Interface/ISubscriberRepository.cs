using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface ISubscriberRepository : IBaseRepository<Subscriber> 
    {
        List<string> GetSubscribingArtistEmailList(string audienceEmail);
        public int GetTotalSubscribers();
        bool CheckAreadyPaid(string audienceEmail, string artistEmail);
        Subscriber GetSubscriber(string accEmail, string creatorEmail);
        Subscriber GetAvaiableSubcriber(string audienceEmail, string creatorEmail);
    }
}
