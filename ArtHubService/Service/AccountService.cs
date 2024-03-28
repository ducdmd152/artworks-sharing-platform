using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubRepository.Repository;
using ArtHubService.Interface;
using ArtHubService.Utils;
using Microsoft.Extensions.Logging;

namespace ArtHubService.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IDapperQueryService dapperQueryService;
        private readonly IUnitOfWork unitOfWork;
		private readonly ILogger<AccountService> logger;

		public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IDapperQueryService dapperQueryService, ILogger<AccountService> logger)
        {
            this.accountRepository = accountRepository;
            this.unitOfWork = unitOfWork;
            this.dapperQueryService = dapperQueryService;
            this.logger = logger;
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
                var accounts = this.accountRepository.GetAccounts();
                accounts.First().Enabled = false;
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
                        Status = 1,
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
                        Status = 1,
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
                        Status = 1,
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
                        Status = 1,
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

        public int GetTotalUsers()
        {
            return this.accountRepository.GetTotalUsers();
        }


        public async Task<bool> DeleteAsync(string email)
        {
            try
            {
                await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false); // mở cửa cho chỉnh sửa db

                var account = this.accountRepository.GetAccount(email);
                account.Enabled = false;
                this.accountRepository.Update(account);

                await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false); // sửa xong rồi đóng lại vă lưu
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return false;
            }
        }
        public Account GetAccountIncludeArtistByEmail(string email)
        {
            return accountRepository.GetAccountIncludeArtistByEmail(email);
        }

        public async Task<Account?> UpdateArtistProfile(AccountUpdateDto accountUpdate)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var dataUpdate = accountRepository.GetAccountIncludeArtistByEmail(accountUpdate.Email);
                dataUpdate.FirstName = accountUpdate.FirstName;
                dataUpdate.LastName = accountUpdate.LastName;
                dataUpdate.Gender = accountUpdate.Gender;
                if (accountUpdate.Avatar != null)
                {
                    dataUpdate.Avatar = accountUpdate.Avatar;
                } else
                {
                    dataUpdate.Avatar = null;
                }
                dataUpdate.Artist!.ArtistName = accountUpdate.ArtistName;
                if (accountUpdate.Bio != null)
                {
                    dataUpdate.Artist!.Bio = accountUpdate.Bio;
                }                
                var updatedArtist = accountRepository.Update(dataUpdate);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return updatedArtist;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return null;
        }
        
        public async Task<Account?> UpdateModeratorProfile(AccountUpdateDto accountUpdate)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var dataUpdate = accountRepository.GetAccount(accountUpdate.Email);
                dataUpdate.FirstName = accountUpdate.FirstName;
                dataUpdate.LastName = accountUpdate.LastName;
                dataUpdate.Gender = accountUpdate.Gender;
                if (accountUpdate.Avatar != null)
                {
                    dataUpdate.Avatar = accountUpdate.Avatar;
                } else
                {
                    dataUpdate.Avatar = null;
                }
            
                var updatedArtist = accountRepository.Update(dataUpdate);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return updatedArtist;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return null;
        }

        public async Task<bool> ChangePassword(PasswordConfirmDto passwordConfirmDto, string email)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);                
                var dataUpdate = accountRepository.GetAccountIncludeArtistByEmail(email);
                dataUpdate.Password = passwordConfirmDto.NewPassword;                
                accountRepository.Update(dataUpdate);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }

        public bool CheckCorrectPassword(string email, string password)
        {
            return accountRepository.CheckCorrectPassword(email, password);
        }

        public async Task<bool> UpdateAccountEnable(string email, bool enable)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                Account account = accountRepository.GetAccountByEmail(email);
                account.Enabled = enable;
                accountRepository.Update(account);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }

        public async Task<bool> CreateAccount(Account account)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await accountRepository.AddAsync(account).ConfigureAwait(false);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
			    return true;
            }
            catch (Exception e)
            {
                this.unitOfWork.RollbackTransaction(); // nếu lưu lỗi thì trả lại dât trước khi sửa
            }

            return false;
        
        }

        public async Task<PageResult<AccountListDTO>> GetListAccountManage(
            SearchAccountConditionDTO search)
        {
            try
            {
                var listPost = this.dapperQueryService
                    .Select<AccountListDTO>(QueryName.GetAccountManagement, search);
                var result = new PageResult<AccountListDTO>
                {
                    PageData = listPost.ToList(),
                    PageInfo = new PageInfo
                    {
                        PageNum = search.PageNumber,
                        PageSize = search.PageSize,
                        TotalPages = listPost.First().TotalPages,
                        TotalItems = listPost.First().TotalItems,
                    }
                };
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public  Account GetAccount(string  email) => accountRepository.GetAccount(email);



        public async Task<bool> UpdateAccount(Account account)
        {
            try
            {
                await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                var existingAccount = this.accountRepository.GetAccount(account.Email);

                if (existingAccount == null)
                {
                    return false; // Account not found
                }

                // Update existing account properties with new values
                existingAccount.FirstName = account.FirstName;
                existingAccount.LastName = account.LastName;
                existingAccount.Gender = account.Gender;
                existingAccount.Status = account.Status;
                existingAccount.Enabled = account.Enabled;

                this.accountRepository.Update(existingAccount);
                await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);

                return true; // Update successful
            }
            catch (Exception ex)
            {
                // Handle exception, rollback transaction if needed
                 this.unitOfWork.RollbackTransaction();
                return false;


            }      
        }

        public Account GetAccountByEmail(string accountEmail)
        {
            return this.accountRepository.GetAccountByEmail(accountEmail);
        }
    }
}
