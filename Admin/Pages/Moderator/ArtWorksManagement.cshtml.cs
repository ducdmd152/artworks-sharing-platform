using System.Runtime.Serialization;
using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using InventoryManagementGUI.Model;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;

namespace Admin.Pages;

public class ArtWorksManagement : PageModel
{
    private readonly IPostService postService;
    private readonly IHelper helper;

    [DataMember]
    [BindProperty]
    public PageResult<PostManagementItem> PageResult { get; set; }
    
    [BindProperty]
    public SearchArtworkManagementConditionDto SearchCondition { get; set; }

    public ArtWorksManagement(IPostService postService, IHelper helper)
    {
        this.postService = postService;
        this.helper = helper;
    }

    public async void OnGet()
    {
         var searchCondition = new SearchArtworkManagementConditionDto();
        searchCondition.PageNumber = 1;
        searchCondition.PageSize = 6;
        this.PageResult = await this.postService.GetListPostOrderByDate(searchCondition).ConfigureAwait(false);
    }

    public async Task<IActionResult> OnPostPaging([FromBody] SearchArtworkManagementConditionDto searchCondition)
    {
        this.PageResult = await this.postService.GetListPostOrderByDate(searchCondition).ConfigureAwait(false);
        var partial1 = this.helper.RenderPartialToStringAsync("/Pages/Shared/_PagingPartial.cshtml", PageResult.PageInfo);
        var partial2 = this.helper.RenderPartialToStringAsync("/Pages/Shared/_ArtWorkListPartial.cshtml", PageResult.PageData);
        return new JsonResult(new { Partial1 = partial1, Partial2 = partial2 });
    }

    public async Task<IActionResult> OnPostSearch([FromBody] SearchArtworkManagementConditionDto searchCondition)
    {
        try
        {
            var tmp = await this.postService.GetListPostOrderByDate(searchCondition).ConfigureAwait(false);
            if (tmp == default || tmp.PageData.Count == 0) throw new Exception();
            this.PageResult = tmp;
            var partial1 = this.helper.RenderPartialToStringAsync("/Pages/Shared/_PagingPartial.cshtml", tmp.PageInfo);
            var partial2 = this.helper.RenderPartialToStringAsync("/Pages/Shared/_ArtWorkListPartial.cshtml", tmp.PageData);
            return new JsonResult(new { Partial1 = partial1, Partial2 = partial2 });
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
    
    public IActionResult OnPostGetProductDetailAsync([FromBody] int postId)
    {
        var result = this.postService.Get(postId);
        if (result == default)
        {
            return new JsonResult(new PostResult()
            {
                Result = Result.Error,
                Data = "Not found!",
            });
        }
        
        return new JsonResult(new PostResult()
        {
            Result = Result.Ok,
            Data = this.ConvertObjectToJson(result),
        });
    }
    
    public async Task<IActionResult> OnPostApprovedOrRejectArtworkAsync([FromBody] ArtworkMode artworkMode)
    {
        Result result = await this.postService.UpdateStatusOfPostAsync(artworkMode.PostId, artworkMode.Mode)
            .ConfigureAwait(false);
        return new JsonResult(new PostResult
        {
            Result = result,
            Data = string.Empty
        }); 
    }

    private string ConvertObjectToJson(object ob)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return JsonConvert.SerializeObject(ob, settings);
    }
}

public class ArtworkMode
{
    public int PostId { get; set; }
    
    public int Mode { get; set; }
}