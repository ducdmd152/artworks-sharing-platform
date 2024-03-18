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

public class ReportManagement : PageModel
{
    private readonly IPostService postService;
    private readonly IReportService reportService;
    private readonly IHelper helper;

    [DataMember]
    [BindProperty]
    public PageResult<ReportManagementItem> PageResult { get; set; }
    
    [BindProperty]
    public SearchArtworkManagementConditionDto SearchCondition { get; set; }

    public ReportManagement(IPostService postService, IHelper helper, IReportService reportService)
    {
        this.postService = postService;
        this.helper = helper;
        this.reportService = reportService;
    }

    public async void OnGet()
    {
         var searchCondition = new SearchReportManagementConditionDto();
        searchCondition.PageNumber = 1;
        searchCondition.PageSize = 5;
        this.PageResult = this.reportService.GetReportList(searchCondition);
    }

    public async Task<IActionResult> OnPostPaging([FromBody] SearchReportManagementConditionDto searchCondition)
    {
        this.PageResult = this.reportService.GetReportList(searchCondition);
        var partial1 = this.helper.RenderPartialToStringAsync("/Pages/Shared/_PagingPartial.cshtml", PageResult.PageInfo);
        var partial2 = this.helper.RenderPartialToStringAsync("/Pages/Shared/_ReportListPartial.cshtml", PageResult.PageData);
        return new JsonResult(new { Partial1 = partial1, Partial2 = partial2 });
    }

    public async Task<IActionResult> OnPostSearch([FromBody] SearchArtworkManagementConditionDto searchCondition)
    {
        return new AcceptedResult();
    }
    
    public IActionResult OnPostGetReportDetailAsync([FromBody] int postId)
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
    
    public async Task<IActionResult> OnPostSkipOrBanPostAsync([FromBody] ReportMode reportMode)
    {
        Result result = await this.reportService.SkipOrBanPostAsync(reportMode.ReportId, reportMode.Mode).ConfigureAwait(false);
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

public class ReportMode
{
    public int ReportId { get; set; }
    
    public int Mode { get; set; }
}