using ArtHubBO.DTO;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator;

public class ProfileModel : PageModel
{
    private readonly IArtistService artistService;

    [BindProperty]
    public ArtistDataDto ArtistData { get; set; }

    public ProfileModel(IArtistService artistService)
    {
        this.artistService = artistService;
    }

    public IActionResult OnGet()
    {
        ArtistData = artistService.GetArtistInforSummaryByCondition();
        //new
        //{
        //    Page = ArtistData.TotalView,
        //    List = 
        //};
        return Page();
    }
}
