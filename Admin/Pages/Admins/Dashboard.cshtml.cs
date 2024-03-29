using ArtHubBO.DTO;
using ArtHubService.Interface;
using ArtHubService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Admins
{
	public class DashboardModel : PageModel
	{
		private readonly ISubscriberService _isubscribersService;
		private readonly IAccountService _accountService;
		private readonly ITransactionService _transactionService;

		public DashboardModel(ISubscriberService isubscribersService, IAccountService accountService, ITransactionService transactionService)
		{
			_isubscribersService = isubscribersService;
			_accountService = accountService;
			_transactionService = transactionService;
		}

		public int TotalSubscribers { get; set; }
		public int TotalUsers { get; set; }
		public double TotalRevue { get; set; }
		public double TotalProfit { get; set; }

		public List<double> ChartData;


		public List<string> ChartLabels { get; set; }
		public List<double> ChartSub { get; set; }
		public async Task OnGetAsync()
		{

			TotalSubscribers = _isubscribersService.GetTotalSubscribers();
			TotalUsers = _accountService.GetTotalUsers();
			TotalRevue = _transactionService.TotalRevenueForApp();
			//
			// TotalProfit = _transactionService.TotalRevenueForApp() * 0.15;
			double commisionRate = await _transactionService.GetCommisionRate();
			TotalProfit = commisionRate * _transactionService.TotalRevenueForApp();

			var transactionsTask = _transactionService.RevenueChartOfYear();
			var transactions = await transactionsTask;
			ChartData = ConvertStatisticOfYearDtoToListDouble(transactions);





			var subChartDataTask = _isubscribersService.GetSubChartOneWeek();
			var subChartData = await subChartDataTask;


			ChartSub = ConvertStatisticOfWekDtoToListDouble(subChartData);



		}

		private List<double> ConvertStatisticOfYearDtoToListDouble(StatisticOfYearDto statisticOfYearDto)
		{
			var list = new List<double>();
			var properties = typeof(StatisticOfYearDto).GetProperties();

			foreach (var property in properties)
			{
				var value = property.GetValue(statisticOfYearDto)?.ToString();
				if (!string.IsNullOrEmpty(value))
				{
					if (double.TryParse(value, out double doubleValue))
					{
						list.Add(doubleValue);
					}
					else
					{
						// Do nothing
					}
				}
			}

			return list;
		}

		private List<double> ConvertStatisticOfWekDtoToListDouble(StatisticOfWeekDto statisticOfYearDto)
		{
			var list = new List<double>();
			var properties = typeof(StatisticOfWeekDto).GetProperties();

			foreach (var property in properties)
			{
				var value = property.GetValue(statisticOfYearDto)?.ToString();
				if (!string.IsNullOrEmpty(value))
				{
					if (double.TryParse(value, out double doubleValue))
					{
						list.Add(doubleValue);
					}
					else
					{
						// Do nothing
					}
				}
			}

			return list;
		}
	}
}
