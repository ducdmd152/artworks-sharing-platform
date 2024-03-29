using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Admins
{
    public class TopCreatorModel : PageModel
    {
        private readonly ITopCreatorService _topCreatorService;
        

        public TopCreatorModel(ITopCreatorService topCreatorService)
        {
            _topCreatorService = topCreatorService;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1; 

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 4;

        public PageResult<TopCreatorDTO> PageResult { get; set; }

        [BindProperty]
        public SearchTopCreatorDTO SearchTopCreator { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
          /*  int startRow = (PageNumber - 1) * PageSize + 1;
            int endRow = PageNumber * PageSize;*/

           
            var searchCondition = new SearchTopCreatorDTO
            {
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            PageResult = await _topCreatorService.GetTopCreator(searchCondition);

          
            return Page();
        }


        public async Task<IActionResult> OnPostSearch()
        {
            try
            {
                // Th?c hi?n tìm ki?m d?a trên searchCriteria
                var searchCondition = new SearchTopCreatorDTO
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    Email = SearchTopCreator.Email
                };
                PageResult = await _topCreatorService.GetTopCreator(searchCondition);

                // Tr? v? k?t qu? tìm ki?m
                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", ex.Message });
            }
        }


        public async Task<IActionResult> OnPostPagingAsync()
        {
            try
            {
               
                var searchCondition = new SearchTopCreatorDTO
                {
                    PageNumber = SearchTopCreator.PageNumber,
                    PageSize = PageSize,
                    Email = SearchTopCreator.Email
                };
                PageResult = await _topCreatorService.GetTopCreator(searchCondition);

                
                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", ex.Message });
            }
        }
    }
}
