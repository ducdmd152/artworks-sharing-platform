using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IAccountRepository : IBaseRepository<Account> 
    {
        IEnumerable<Account> GetAccounts();
        Account? GetAccountsIncludeRoleByEmailPassword(string username, string password);
        Account GetAccount(string postArtistEmail);
        Account GetAccountIncludeArtistByEmail(string email);
        bool CheckCorrectPassword(string email, string password);
        Account GetAccountByEmail(string email);
    }
}
