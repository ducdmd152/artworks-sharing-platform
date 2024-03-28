using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator;

public class ProfileModel : PageModel
{
    private readonly IArtistService artistService;
    private readonly IAccountService accountService;

    [BindProperty]
    public PageResult<ArtistDataDto> ArtistData { get; set; }

    public ProfileModel(IArtistService artistService, IAccountService accountService)
    {
        this.artistService = artistService;
        this.accountService = accountService;
    }

    public async Task<IActionResult> OnGet()
    {
        var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");
        SearchPayload<ArtistSearchConditionDto> searchPayload = new SearchPayload<ArtistSearchConditionDto>();
        searchPayload.PageInfo = new PageInfo
        {
            PageNum = 1,
            PageSize = 6,            
        };
        searchPayload.SearchCondition = new ArtistSearchConditionDto
        {
            IsGetDataPost = true,
            Email = accountEmail ?? "",
            PostScope = new int[] { (int)PostScope.Public, (int)PostScope.Subscriber, (int)PostScope.Private },
            PostStatus = new int[] { (int)PostStatus.Pending, (int)PostStatus.Approval, (int)PostStatus.Reject, (int)PostStatus.Repending },
            IsOrderByReact = true,
            IsOrderByView = false,
            IsOrderByTitle = false,
            IsOrderAsc = false,
            AccountStatus = 1,
            AccountIsEnable = true
        };
        var artistData = await artistService.GetArtistInforSummaryByCondition(searchPayload);
        if (artistData.PageData.Count <= 0) { 
            var artist = accountService.GetAccountIncludeArtistByEmail(accountEmail!);
            artistData.PageData = new List<ArtistDataDto> {
                new ArtistDataDto {
                    Email = artist.Email,
                    Avatar = artist.Avatar,
                    ArtistName = artist.Artist!.ArtistName,
                    Bio = artist.Artist.Bio,
                    TotalSubscriber = artist.Artist.TotalSubscribe,
                } 
            };
            ArtistData = artistData;
        } else
        {
            ArtistData = artistData;
        }        
        return Page();
    }
}
