/*using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages
{
    public class AccountManagementModel : PageModel
    {
        private readonly IAccountService _accountService;
        //   private readonly 




        public AccountManagementModel(IAccountService accountService)
        {
            _accountService = accountService;
        }


        //   public SearchAccountConditionDTO SearchCondition { get; set; }
        public PageResult<AccountListDTO> PageResult { get; set; }


        *//* public async void OnGet()
         {
             var searchCondition = new SearchAccountConditionDTO();
             searchCondition.PageNumber = 1;
             searchCondition.PageSize = 5;
             this.PageResult = await this._accountService.GetListAccountManage(searchCondition);

         }*//*
        public async Task OnGet()
        {
            var searchCondition = new SearchAccountConditionDTO();
            searchCondition.PageNumber = 1;
            searchCondition.PageSize = 10;


            this.PageResult = await this._accountService.GetListAccountManage(searchCondition).ConfigureAwait(false);
        }


        *//* public IActionResult OnPostDelete(string email)
         {

             return Page();
         }*//*

        public async Task<IActionResult> OnPostDelete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

           
            bool deleteResult = await _accountService.DeleteAsync(email).ConfigureAwait(false);

            if (deleteResult)
            {
              
                return RedirectToPage();
            }
            else
            {
                
                return Page();
            }
        }

    }
}*/

using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Admin.Pages.Admins
{
    public class AccountManagementModel : PageModel
    {
        private readonly IAccountService _accountService;

        public AccountManagementModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1; // Số trang mặc định là 1

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10; // Kích thước trang mặc định là 10

        public PageResult<AccountListDTO> PageResult { get; set; }
        [BindProperty]
        public SearchAccountConditionDTO SearchAccountCondition { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Tính toán vị trí bắt đầu và kết thúc của dữ liệu trên trang hiện tại
            int startRow = (PageNumber - 1) * PageSize + 1;
            int endRow = PageNumber * PageSize;

            // Lấy dữ liệu từ dịch vụ hoặc database
            var searchCondition = new SearchAccountConditionDTO
            {
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            PageResult = await _accountService.GetListAccountManage(searchCondition);

            // Trả về dữ liệu phân trang cho giao diện người dùng
            return Page();
        }


        public async Task<IActionResult> OnPostSearch()
        {
            try
            {
                // Thực hiện tìm kiếm dựa trên searchCriteria
                var searchCondition = new SearchAccountConditionDTO
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    Email = this.SearchAccountCondition.Email
                };
                PageResult = await _accountService.GetListAccountManage(searchCondition);

                // Trả về kết quả tìm kiếm
                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", Message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostPagingAsync()
        {
            try
            {
                // Thực hiện tìm kiếm dựa trên searchCriteria
                var searchCondition = new SearchAccountConditionDTO
                {
                    PageNumber = this.SearchAccountCondition.PageNumber,
                    PageSize = PageSize,
                    Email = this.SearchAccountCondition.Email
                };
                PageResult = await _accountService.GetListAccountManage(searchCondition);

                // Trả về kết quả tìm kiếm
                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", Message = ex.Message });
            }
        }


        public async Task<IActionResult> OnPostDetailAsync()
        {
            try
            {
                // Thực hiện tìm kiếm dựa trên searchCriteria
                var searchCondition = new SearchAccountConditionDTO
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    Email = this.SearchAccountCondition.Email
                };
                PageResult = await _accountService.GetListAccountManage(searchCondition);

                // Trả về kết quả tìm kiếm
                return Page();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = "error", Message = ex.Message });
            }
        }

        public async Task<IActionResult> OnPostDelete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }


            bool deleteResult = await _accountService.DeleteAsync(email).ConfigureAwait(false);

            if (deleteResult)
            {

                return RedirectToPage();
            }
            else
            {

                return Page();
            }
        }




		public async Task<IActionResult> OnPostRestore(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				return BadRequest();
			}


			bool deleteResult = await _accountService.RetoreAsync(email).ConfigureAwait(false);

			if (deleteResult)
			{

				return RedirectToPage();
			}
			else
			{

				return Page();
			}
		}



		public async Task<IActionResult> OnPostUpdate(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				return BadRequest();
			}


			bool deleteResult = await _accountService.UpdateAccountStatus(email).ConfigureAwait(false);

			if (deleteResult)
			{

				return RedirectToPage();
			}
			else
			{

				return Page();
			}
		}
	}

}

