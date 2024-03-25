using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
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
    }
}
