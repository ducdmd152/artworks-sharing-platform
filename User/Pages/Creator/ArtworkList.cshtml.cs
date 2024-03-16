using ArtHubBO.Constants;
using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace User.Pages.Creator
{
    public class ArtworkListModel : PageModel
    {
        private readonly IPostService postService;

        private readonly ICategoryService categoryService;

        [BindProperty]
        public SearchPayload<PostSearchConditionDto> SearchPayload { get; set; } = new SearchPayload<PostSearchConditionDto>(new PageInfo(1, PageConstants.PageSize), new PostSearchConditionDto());

        [BindProperty]
        public DateTime Today { get; set; } = DateTime.Now;
        [BindProperty]
        public DateTime? DateFilter { get; set; } = null;
        [BindProperty]
        public PostStatus PostStatusPending { get; } = PostStatus.Pending;
        [BindProperty]
        public PostStatus PostStatusApproval { get; } = PostStatus.Approval;
        [BindProperty]
        public PostStatus PostStatusReject { get; } = PostStatus.Reject;
        [BindProperty]
        public PostStatus PostStatusRepending { get; } = PostStatus.Repending;
        [BindProperty]
        public PostScope PostScopePublic { get; } = PostScope.Public;
        [BindProperty]
        public PostScope PostScopeSubscriber { get; } = PostScope.Subscriber;
        [BindProperty]
        public PostScope PostScopePrivate { get; } = PostScope.Private;
        [BindProperty]
        public SortDirection SortASC { get; } = SortDirection.ASC;
        [BindProperty]
        public SortDirection SortDESC { get; } = SortDirection.DESC;
        [BindProperty]
        public SortType TypeRecent { get; } = SortType.RECENT;
        [BindProperty]
        public List<Category> Categories { get; set; }

        [BindProperty]
        public PageResult<Post> PageResult { get; set; } = new PageResult<Post> { };

        public ArtworkListModel(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public async Task OnGetAsync()
        {
            var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");     
            if (accountEmail != null)
            {
                SearchPayload.SearchCondition.ArtistEmail = accountEmail;
            }
            SearchPayload.SearchCondition.SortDirection = SortDirection.DESC;
            SearchPayload.SearchCondition.SortType = SortType.RECENT;
            Categories = categoryService.GetCategories().ToList();                        
            var output = await postService.GetAllPostBySearchConditionAsync(SearchPayload);
            if (output != null && output.Count > 0)
            {
                PageResult = PageResult.Build(SearchPayload.PageInfo, SearchPayload.PageInfo.TotalItems, output.ToList());
            } else
            {
                PageResult = PageResult.PageNoData(SearchPayload.PageInfo);
            }            
            return;
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {                        
            var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");
            if (accountEmail != null)
            {
                SearchPayload.SearchCondition.ArtistEmail = accountEmail;
            }
            SearchPayload.SearchCondition.SortType = SortType.RECENT;
            var postStatus = Request.Form["PostStatus"];
            if (Enum.TryParse(postStatus, out PostStatus parsedStatus)) {
                SearchPayload.SearchCondition.PostStatus = parsedStatus;
            }     
            var postScope = Request.Form["PostScope"];
            if (Enum.TryParse(postScope, out PostScope parsedScope))
            {
                SearchPayload.SearchCondition.PostScope = parsedScope;
            }
            var selectedCategoryIds = Request.Form["SelectedCategories"].Select(categoryId =>
            {
                if (int.TryParse(categoryId, out int parsedCategoryId))
                {
                    return parsedCategoryId;
                }
                else
                {                   
                    return -1;
                }
            })
            .Where(categoryId => categoryId != -1)
            .ToArray();

            var sortDirection = Request.Form["SortDirection"];
            if (Enum.TryParse(sortDirection, out SortDirection parsedSortDirection))
            {
                SearchPayload.SearchCondition.SortDirection = parsedSortDirection;
            }

            SearchPayload.SearchCondition.CategoryId = selectedCategoryIds;
            var dateFilter = Request.Form["DateFilter"];
            if (DateTime.TryParse(dateFilter, out DateTime parsedDateFilter))
            {
                SearchPayload.SearchCondition.CreatedDate = parsedDateFilter;
                DateFilter = null;
            }
            
            var customDate = Request.Form["CustomFilterDate"];
            if (DateTime.TryParse(customDate, out DateTime parsedCustomDate))
            {
                SearchPayload.SearchCondition.CreatedDate = parsedCustomDate;
                DateFilter = parsedCustomDate;
            }


            var output = await postService.GetAllPostBySearchConditionAsync(SearchPayload);
            if (output != null && output.Count > 0)
            {
                PageResult = PageResult.Build(SearchPayload.PageInfo, SearchPayload.PageInfo.TotalItems, output.ToList());
            }
            else
            {
                PageResult = PageResult.PageNoData(SearchPayload.PageInfo);
            }

            return Partial("_ArtworkListPartial", PageResult);
        }
    }
}
