using System.Runtime.Serialization;
using ArtHubBO.DTO;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using InventoryManagementGUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages;

public class ArtWorksManagement : PageModel
{
    private readonly IPostService postService;

    [DataMember]
    [BindProperty]
    public PageResult<PostManagementItem> PageResult { get; set; }
    
    [BindProperty]
    public SearchArtworkManagementConditionDto SearchCondition { get; set; }

    public ArtWorksManagement(IPostService postService)
    {
        this.postService = postService;
    }

    public async void OnGet()
    {
         var searchCondition = new SearchArtworkManagementConditionDto();
        searchCondition.PageNumber = 1;
        searchCondition.PageSize = 5;
        this.PageResult = await this.postService.GetListPostOrderByDate(searchCondition).ConfigureAwait(false);
    }

    public async Task<IActionResult> OnPostPaging([FromBody] SearchArtworkManagementConditionDto searchCondition)
    {
        this.PageResult = await this.postService.GetListPostOrderByDate(searchCondition).ConfigureAwait(false);
        return Partial("_ArtWorkListPartial", PageResult.PageData);
    }

    public async Task<IActionResult> OnPostSearch([FromBody] SearchArtworkManagementConditionDto searchCondition)
    {
        try
        {
            var tmp = await this.postService.GetListPostOrderByDate(searchCondition).ConfigureAwait(false);
            if (tmp == default) throw new Exception();
            this.PageResult = tmp;
            return Partial("_ArtWorkListPartial", tmp.PageData);
        }
        catch
        {
            return new JsonResult(new PostResult()
            {
                Result = Result.Error,
                Data = "Not found!",
            });
        }
    }
}