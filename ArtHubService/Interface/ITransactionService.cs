using ArtHubBO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Interface
{
    public interface ITransactionService
    {
        public double TotalRevenueForApp();
        IEnumerable<Transaction> GetTransactions();


    }
}
