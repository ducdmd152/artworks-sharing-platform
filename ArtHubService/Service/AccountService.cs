using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using ArtHubService.Utils;

namespace ArtHubService.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUnitOfWork unitOfWork;
        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            this.accountRepository = accountRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return this.accountRepository.GetAccounts();
        }

        public Account? GetAccountByUsernameAndPassword(string email, string password)
        {            
            var decryptPassword = Encryption.Encrypt(password);
            return this.accountRepository.GetAccountsIncludeRoleByEmailPassword(email, decryptPassword);
        }

        public async Task<Account> UpdateAsync()
        {
            try
            {
                await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var accounts = this.GetAccounts();
                foreach (var account in accounts)
                {
                    account.Avatar = "avater new";
                }

                var accountNew = accountRepository.UpdateRange(accounts);
                await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                
                return accountNew.FirstOrDefault();
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }

            return null;
        }

        public async Task<bool> RegisterAccountAsync()
        {
            try
            {
                await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var accounts = new List<Account>()
                {
                    new Account()
                    {
                        Email = "Account 1",
                        Password = "1",
                        FirstName = "2",
                        LastName = "3",
                        Gender = Gender.Female.ToString(),
                        Status = "ok",
                        Avatar = "dep gai",
                        RoleId = 0,
                    },
                    new Account()
                    {
                        Email = "Account 2",
                        Password = "1",
                        FirstName = "2",
                        LastName = "3",
                        Gender = Gender.Male.ToString(),
                        Status = "ok",
                        Avatar = "dep trai",
                        RoleId = 0,
                    }
                };

                await this.accountRepository.AddRangeAsync(accounts).ConfigureAwait(false);
                await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }

            return false;
        }

        public async Task<bool> RemoveAccountAsync()
        {
            try
            {
                await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var accounts = new List<Account>()
                {
                    new Account()
                    {
                        Email = "Account 1",
                        Password = "1",
                        FirstName = "2",
                        LastName = "3",
                        Gender = Gender.Female.ToString(),
                        Status = "ok",
                        Avatar = "dep gai",
                        RoleId = 0,
                    },
                    new Account()
                    {
                        Email = "Account 2",
                        Password = "1",
                        FirstName = "2",
                        LastName = "3",
                        Gender = Gender.Male.ToString(),
                        Status = "ok",
                        Avatar = "dep trai",
                        RoleId = 0,
                    }
                };
                
                this.accountRepository.RemoveRange(accounts);
                await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction();
            }

            return false;
        }
    }
}
