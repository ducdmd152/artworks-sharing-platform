using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator;

public class EditPostModel : PageModel
{
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;

    [BindProperty]
    public Post Post { get; set; }
    [BindProperty]
    public List<Category> Categories { get; set; }
    [BindProperty]
    public PostScope PostScopePublic { get; } = PostScope.Public;
    [BindProperty]
    public PostScope PostScopeSubscriber { get; } = PostScope.Subscriber;
    [BindProperty]
    public PostScope PostScopePrivate { get; } = PostScope.Private;
    [BindProperty]
    public IFormFile FileUpload { get; set; }

    public EditPostModel(IPostService postService, ICategoryService categoryService)
    {
        this.postService = postService;
        this.categoryService = categoryService;
    }

    public void OnGet(int postId)
    {
        Post = postService.Get(postId);
        Categories = categoryService.GetCategories().ToList();
    }


}
