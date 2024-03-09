using System.Runtime.Serialization;
using ArtHubBO.DTO;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages;

public class ArtWorksManagement : PageModel
{
    private readonly IPostService postService;

    [DataMember]
    [BindProperty]
    public IEnumerable<PostManagementItem> ListPost { get; set; }
    
    [BindProperty]
    public SearchArtworkManagementConditionDto SearchCondition { get; set; }

    public ArtWorksManagement(IPostService postService)
    {
        this.postService = postService;
    }

    public async void OnGet()
    {
        this.ListPost = await this.postService.GetListPostOrderByDate(SearchCondition).ConfigureAwait(false);
    }
}