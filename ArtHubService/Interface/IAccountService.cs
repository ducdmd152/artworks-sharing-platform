using ArtHubBO.Entities;

namespace ArtHubService.Interface
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();

        public Task<Account> UpdateAsync();

        public Task<bool> RegisterAccountAsync();

        public Task<bool> RemoveAccountAsync();

        public int GetTotalUsers();
    }
}
