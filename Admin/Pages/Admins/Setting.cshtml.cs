using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Admins
{
    public class SettingModel : PageModel
    {
        private readonly ISystemConfigService systemConfigService;

        public SettingModel(ISystemConfigService systemConfigService)
        {
            this.systemConfigService = systemConfigService;
        }

        [BindProperty]
        public double CommisionRate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CommisionRate = await systemConfigService.GetCommisionRateAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await systemConfigService.UpdateCommisionRateAsync(CommisionRate);

            return Page();
        }
    }
}
