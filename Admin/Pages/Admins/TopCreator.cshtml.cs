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
        public int PageNumber { get; set; } = 1; // S? trang m?c ??nh l� 1

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public PageResult<TopCreatorDTO> PageResult { get; set; }

        [BindProperty]
        public SearchTopCreatorDTO SearchTopCreator { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // T�nh to�n v? tr� b?t ??u v� k?t th�c c?a d? li?u tr�n trang hi?n t?i
            int startRow = (PageNumber - 1) * PageSize + 1;
            int endRow = PageNumber * PageSize;

            // L?y d? li?u t? d?ch v? ho?c database
            var searchCondition = new SearchTopCreatorDTO
            {
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            PageResult = await _topCreatorService.GetTopCreator(searchCondition);

            // Tr? v? d? li?u ph�n trang cho giao di?n ng??i d�ng
            return Page();
        }


        public async Task<IActionResult> OnPostSearch()
        {
            try
            {
                // Th?c hi?n t�m ki?m d?a tr�n searchCriteria
                var searchCondition = new SearchTopCreatorDTO
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    Email = SearchTopCreator.Email
                };
                PageResult = await _topCreatorService.GetTopCreator(searchCondition);

                // Tr? v? k?t qu? t�m ki?m
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
                // Th?c hi?n t�m ki?m d?a tr�n searchCriteria
                var searchCondition = new SearchTopCreatorDTO
                {
                    PageNumber = SearchTopCreator.PageNumber,
                    PageSize = PageSize,
                    Email = SearchTopCreator.Email
                };
                PageResult = await _topCreatorService.GetTopCreator(searchCondition);

                // Tr? v? k?t qu? t�m ki?m
                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", ex.Message });
            }
        }
    }
}
