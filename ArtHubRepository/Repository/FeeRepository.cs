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
    }
}
