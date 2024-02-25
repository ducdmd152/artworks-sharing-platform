using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IAccountRepository : IBaseRepository<Account> 
    {
        IEnumerable<Account> GetAccounts();
    }
}
