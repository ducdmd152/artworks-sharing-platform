using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;

namespace ArtHubRepository.Repository
{
    public class FeeRepository : BaseRepository<Fee>, IFeeRepository
    {
        public FeeRepository(IBaseDAO<Fee> baseDAO) : base(baseDAO)
        {
        }

        public double GetFeeByCreatorEmail(string creatorEmail)
            => this.DbSet.First(x => x.ArtistEmail == creatorEmail).Amount;
        
        public Fee GetFullFeeByCreatorEmail(string creatorEmail)
            => this.DbSet.First(x => x.ArtistEmail == creatorEmail);
        
    }
}
