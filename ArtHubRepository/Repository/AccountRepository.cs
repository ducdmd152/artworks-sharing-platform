using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

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

        public Account? GetAccountsIncludeRoleByEmailPassword(string email, string password)
        {
            return this.DbSet.Include(a => a.Role).FirstOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password));
        }

		public int GetTotalUsersWithinLast30Days()
		{
			DateTime thirtyDaysAgo = DateTime.Today.AddDays(-30);
			return this.DbSet.Count(p => p.RoleId != 4 && p.RoleId != 3 && p.CreatedDate >= thirtyDaysAgo);
		}

		public Account GetAccount(string postArtistEmail)
            => this.DbSet.FirstOrDefault(x => x.Email == postArtistEmail);

        public IEnumerable<Account> GetCreatorAndUser()
        {
            return this.DbSet.Where(p => p.RoleId != 1);
        }
        public Account GetAccountIncludeArtistByEmail(string email)
        {
            return this.DbSet.Include(a => a.Artist).Include(a => a.Role).First(a => a.Email.Equals(email));
        }

        public bool CheckCorrectPassword(string email, string password)
        {
            return this.DbSet.Any(a => a.Email.Equals(email) & a.Password.Equals(password));
        }

        public Account? GetAccountByEmail(string email)
        {
            return this.DbSet.Where(a => a.Email == email).FirstOrDefault();
        }
    }
}
