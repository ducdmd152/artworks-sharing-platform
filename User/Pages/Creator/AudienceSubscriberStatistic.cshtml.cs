using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator
{
    public class AudienceSubscriberStatisticModel : PageModel
    {
        private readonly ISubscriberService subscriberService;
        private readonly ITransactionService transactionService;
        
        public StatisticOfWeekDto StatisticOfWeek { get; private set; }
        public StatisticOfYearDto StatisticOfYear { get; private set; }
        public string Type { get; private set; } = "Subscriber";

        public AudienceSubscriberStatisticModel(ISubscriberService subscriberService, ITransactionService transactionService)
        {
            this.subscriberService = subscriberService;
            this.transactionService = transactionService;
        }

        public async Task<IActionResult> OnGet(string type)
        {
            if (type != null)
            {
                Type = type;
            }
            var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL")!;
            if (Type == "Subscriber")
            {
                StatisticOfWeek = await subscriberService.GetStatisticOfSubscriberLastWeek(accountEmail);
                StatisticOfYear = await subscriberService.GetStatisticOfSubscriberMonthOfYear(accountEmail);
            } else
            {
                StatisticOfWeek = await transactionService.GetStatisticOfRevenueLastWeek(accountEmail);
                StatisticOfYear = await transactionService.GetStatisticOfRevenueMonthOfYear(accountEmail);
            }                        
            return Page();
        }
    }
}
