using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Admins
{
    public class TopArtWorkModel : PageModel
    {
        private readonly ITopArtWorkService _topArtWorkService;




        public TopArtWorkModel(ITopArtWorkService topArtWorkService)
        {
            _topArtWorkService = topArtWorkService;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1; // S? trang m?c ??nh là 1

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public PageResult<TopArtWorkDTO> PageResult { get; set; }

        [BindProperty]
        public SearchArtWorkDTO SearchTopArtWork { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
/*            int startRow = (PageNumber - 1) * PageSize + 1;
            int endRow = PageNumber * PageSize;*/

          
            var searchCondition = new SearchArtWorkDTO
            {
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            PageResult = await _topArtWorkService.GetArtWork(searchCondition);

            
            return Page();
        }


        public async Task<IActionResult> OnPostSearch()
        {
            try
            {
                
                var searchCondition = new SearchArtWorkDTO
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    PostTitle = SearchTopArtWork.PostTitle
                };
                PageResult = await _topArtWorkService.GetArtWork(searchCondition);

            
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

                var searchCondition = new SearchArtWorkDTO
                {
                    PageNumber = SearchTopArtWork.PageNumber,
                    PageSize = PageSize,
                    PostTitle = SearchTopArtWork.PostTitle
                };
                PageResult = await _topArtWorkService.GetArtWork(searchCondition);


                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", ex.Message });
            }
        }
    }
}

