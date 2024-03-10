using ArtHubBO.Entities;

namespace ArtHubService.Interface
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();

        Account? GetAccountByUsernameAndPassword(string email, string password);

        public Task<Account> UpdateAsync();

        public Task<bool> RegisterAccountAsync();

        public Task<bool> RemoveAccountAsync();
    }
}
