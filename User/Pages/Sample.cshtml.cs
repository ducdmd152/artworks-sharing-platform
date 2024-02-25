using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages
{
    public class SampleModel : PageModel
    {
        private readonly IAccountService accountService;
        private IDapperQueryService dapperQueryService;

        public SampleModel(IAccountService accountService, IDapperQueryService dapperQueryService)
        {
            this.accountService = accountService;
            this.dapperQueryService = dapperQueryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // get by repo
            var listAccount = this.accountService.GetAccounts();
            
            //get by sql
            var account = this.dapperQueryService.Query<string>(QueryName.FirstQuery, default);
            
            // update
            var temp = await this.accountService.UpdateAsync().ConfigureAwait(false);

            // register
            var temp2 = await this.accountService.RegisterAccountAsync().ConfigureAwait(false);

            // remove
            var temp3 = await this.accountService.RemoveAccountAsync().ConfigureAwait(false);
            
            return this.Page();
        }
    }
}
