using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ISubcribersService _isubscribersService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public DashboardModel(ISubcribersService isubscribersService, IAccountService accountService, ITransactionService transactionService)
        { 
            _isubscribersService = isubscribersService;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public int TotalSubscribers { get; set; }
        public int TotalUsers { get; set; }
         public double TotalRevue { get; set; }
        public double TotalProfit { get; set; }

		public List<int> ChartData { get; set; }
		public List<string> ChartLabels { get; set; }
		public void OnGetAsync()
        {
            TotalSubscribers =  _isubscribersService.GetTotalSubscribers();
            TotalUsers = _accountService.GetTotalUsers();
            TotalRevue = _transactionService.TotalRevenueForApp();
            TotalProfit = _transactionService.TotalRevenueForApp() * 0.15;

			var transactions = _transactionService.GetTransactions().ToList();
			ChartData = transactions.Select(t => (int)t.Amount).ToList();
			
        }
    }
}
