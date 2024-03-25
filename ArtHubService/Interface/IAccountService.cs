using ArtHubBO.DTO;
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

        Account GetAccountIncludeArtistByEmail(string email);

        Task<Account?> UpdateArtistProfile(AccountUpdateDto accountUpdate);

        Task<bool> ChangePassword(PasswordConfirmDto passwordConfirmDto, string email);
        bool CheckCorrectPassword(string email, string password);
        Task<bool> UpdateAccountEnable(string email, bool enable);

        Task<bool> CreateAccount(Account account);
    }
}
