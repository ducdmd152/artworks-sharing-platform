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

        public Account GetAccount(string postArtistEmail)
            => this.DbSet.FirstOrDefault(x => x.Email == postArtistEmail);
    }
}
