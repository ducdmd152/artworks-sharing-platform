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
    //    public  Task<bool> UpdateAccount(Account account);


        Account GetAccountIncludeArtistByEmail(string email);

        Task<Account?> UpdateArtistProfile(AccountUpdateDto accountUpdate);
        Task<Account?> UpdateProfile(AccountUpdateDto accountUpdate);

        Task<bool> ChangePassword(PasswordConfirmDto passwordConfirmDto, string email);
        bool CheckCorrectPassword(string email, string password);
        

        Task<bool> CreateAccount(Account account);
        Account GetAccountByEmail(string accountEmail);

        public void Update(Account account);


        Task<bool> UpdateAccountEnable(string email, bool enable);

        public Task<bool> UpdateAccountStatus(string email);

        public Task<bool> RetoreAsync(string email); 



	}
}
