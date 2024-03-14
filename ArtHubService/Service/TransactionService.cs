using ArtHubBO.Entities;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _itransactionRepository;

        public TransactionService(ITransactionRepository itransactionRepository)
        {
            _itransactionRepository = itransactionRepository;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
           return _itransactionRepository.GetTransactions();
        }

        public double TotalRevenueForApp()
        {
                return _itransactionRepository.TotalRevenueForApp();
         
        }

    }
}
