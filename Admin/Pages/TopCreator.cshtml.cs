using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages
{
    public class TopCreatorModel : PageModel
    {
        private readonly ITopCreatorService _topCreatorService;




        public TopCreatorModel(ITopCreatorService topCreatorService)
        {
            _topCreatorService = topCreatorService;
        }


        public PageResult<TopCreatorDTO> PageResult { get; set; }


      
        public async Task OnGet()
        {
            var searchCondition = new SearchTopCreatorDTO();
            searchCondition.PageNumber = 1;
            searchCondition.PageSize = 10;


            this.PageResult = await this._topCreatorService.GetTopCreator(searchCondition).ConfigureAwait(false);
        }


        /* public IActionResult OnPostDelete(string email)
         {

             return Page();
         }*/

      

    }
}
