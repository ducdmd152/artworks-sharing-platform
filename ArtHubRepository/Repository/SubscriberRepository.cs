using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtHubRepository.Repository;

public class SubscriberRepository : BaseRepository<Subscriber>, ISubscriberRepository
{
    public SubscriberRepository(IBaseDAO<Subscriber> baseDAO) : base(baseDAO)
    {        
    }
    public int GetTotalSubscribers()
    {
        return this.DbSet.Count();
    }
        
    public List<string> GetSubscribingArtistEmailList(string audienceEmail)
    {
    return this.DbSet.Where(item => item.EmailUser.ToLower().Equals(audienceEmail.ToLower())
                                    && item.Status == 1
                                    && DateTime.Now <= item.ExpiredDate
                                    && item.EmailArtistNavigation.Account.Enabled)
                        .Select(item => item.EmailArtist)
                        .ToList(); 



   	}
}
