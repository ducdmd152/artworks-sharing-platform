using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubService.Interface
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();

        Account? GetAccountByUsernameAndPassword(string email, string password);

        public Task<Account> UpdateAsync();

        public Task<bool> RegisterAccountAsync();

        public Task<bool> RemoveAccountAsync();

        public int GetTotalUsers();

        // Account GetAccount(string postArtistEmail);
        public Account GetAccount(string email);
        public  Task<bool> DeleteAsync(string email);

          Task<PageResult<AccountListDTO>> GetListAccountManage(SearchAccountConditionDTO search);

    }
}
