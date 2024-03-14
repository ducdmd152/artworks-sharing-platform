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

        public int GetTotalUsers()
        {
            return this.DbSet.Count(p => p.RoleId != 2 && p.RoleId != 3);
        }
    }
}
