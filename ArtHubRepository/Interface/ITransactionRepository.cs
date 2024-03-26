using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface ITransactionRepository : IBaseRepository<Transaction> 
    {
        public double TotalRevenueForApp();

        IEnumerable<Transaction> GetTransactions();
    }
}
