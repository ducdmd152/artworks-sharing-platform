using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtHubRepository.Repository
{
    public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(IBaseDAO<Reaction> baseDAO) : base(baseDAO)
        {
        }

        public IEnumerable<Reaction> GetReactions()
        {
            return this.DbSet.AsNoTracking().ToList();
        }        
    }
}
