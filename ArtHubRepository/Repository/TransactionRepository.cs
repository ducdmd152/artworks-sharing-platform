using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;

namespace ArtHubRepository.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IBaseDAO<Transaction> baseDAO) : base(baseDAO)
        {
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return this.DbSet.ToList();
        }

        public double TotalRevenueForApp()
        {
            DateTime currentDate = DateTime.Now;
           
            DateTime thirtyDaysAgo = currentDate.AddDays(-30);

          
            double totalRevenue = this.DbSet
                .Where(transaction => transaction.CreatedDate >= thirtyDaysAgo && transaction.CreatedDate <= currentDate)
                .Sum(transaction => transaction.Amount);

            return totalRevenue;

        }

       
    }
}
