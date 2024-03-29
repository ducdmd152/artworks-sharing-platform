using ArtHubBO.Entities;
using ArtHubBO.Enum;
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

    public bool CheckAreadyPaid(string audienceEmail, string artistEmail)
    {
        return this.DbSet
            .Any(x => x.EmailArtist == artistEmail 
                      && x.EmailUser == audienceEmail 
                      && x.CreatedDate.Date == DateTime.Now.Date
                      && x.Status == (int)SubscriberStatus.Subscribed);
    }

    public Subscriber GetSubscriber(string accEmail, string creatorEmail)
    {
        return this.DbSet
            .Include(x => x.Transactions)
            .Where(s => s.EmailArtist == creatorEmail && s.EmailUser == accEmail)
            .OrderByDescending( x => x.CreatedDate)
            .FirstOrDefault();
    }

    public Subscriber GetAvaiableSubcriber(string audienceEmail, string creatorEmail)
    {
        return this.DbSet
            .FirstOrDefault(s => s.EmailArtist == creatorEmail
                                 && s.EmailUser == audienceEmail
                                 && s.ExpiredDate > DateTime.Now);
    }
    
    public List<string> GetSubscribingArtistEmailList(string audienceEmail)
    {
    return this.DbSet.Where(item => item.EmailUser.ToLower().Equals(audienceEmail.ToLower())
                                    //&& item.Status == 1
                                    && DateTime.Now <= item.ExpiredDate
                                    && item.EmailArtistNavigation.Account.Enabled)
                        .Select(item => item.EmailArtist)
                        .ToList(); 



   	}
   	
   		public int GetTotalSubscribersWithinLast30Days()
	{
		DateTime thirtyDaysAgo = DateTime.Today.AddDays(-30);
		return this.DbSet.Count(sub => sub.CreatedDate >= thirtyDaysAgo);
	}

}
