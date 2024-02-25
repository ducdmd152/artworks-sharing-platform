using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;

namespace ArtHubRepository.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IBaseDAO<Account> baseDAO) : base(baseDAO)
        {
        }

        public IEnumerable<Account> GetAccounts()
        {
            return this.DbSet.ToList();
        }
    }
}
