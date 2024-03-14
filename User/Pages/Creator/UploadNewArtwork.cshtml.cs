using ArtHubBO.Constants;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Models;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator;

public class UploadNewArtworkModel : PageModel
{
    private readonly IConfiguration configuration;
    private readonly IStorageService storageService;
    private readonly ICategoryService categoryService;
    private readonly IPostService postService;

    [BindProperty]
    public IFormFile FileUpload { get; set; }

    [BindProperty]
    public List<Category> Categories { get; set; }

    [BindProperty]
    public PostScope PostScopePublic { get; } = PostScope.Public;
    [BindProperty]
    public PostScope PostScopeSubscriber { get; } = PostScope.Subscriber;
    [BindProperty]
    public PostScope PostScopePrivate { get; } = PostScope.Private;

    [BindProperty]
    public Post Post { get; set; }

    public UploadNewArtworkModel(IConfiguration configuration, IStorageService storageService, ICategoryService categoryService, IPostService postService)
    {
        this.configuration = configuration;
        this.storageService = storageService;
        this.categoryService = categoryService;
        this.postService = postService;
    }

    public async Task<IActionResult> OnGet()
    {
        Categories = categoryService.GetCategories().ToList();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");
        if (accountEmail != null)
        {
            Post.ArtistEmail = accountEmail;
        }
        await using var memoryStream = new MemoryStream();
        await FileUpload.CopyToAsync(memoryStream);
        var fileExt = Path.GetExtension(FileUpload.FileName);
        var objName = $"{Guid.NewGuid()}{fileExt}";
        var s3Obj = new S3ObjectModel()
        {
            BucketName = configuration[S3Constants.BucketName] ?? "",
            InputStream = memoryStream,
            Name = objName
        };

        var credential = new AwsCredentials()
        {
            AwsKey = configuration[S3Constants.AccessKey] ?? "",
            AwsSecret = configuration[S3Constants.SecretKey] ?? ""
        };
        var responseUploadImage = await storageService.UploadFileAsync(s3Obj, credential);

        var postScope = Request.Form["PostScope"];
        if (Enum.TryParse(postScope, out PostScope parsedScope))
        {
            Post.Scope = (int)parsedScope;
        }
        Post.Status = (int)PostStatus.Pending;

        
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

        // Post category
        List<PostCategory> postCategories = new List<PostCategory>();
        foreach(var id in selectedCategoryIds) {
            postCategories.Add(new PostCategory
            {
                CategoryId = id
            });
        }
        //Image of post
        Image image = new Image()
        {
            Type = fileExt.Substring(1),
            ImageUrl = responseUploadImage.LinkSource,
            DeleteFlag = false
        };
        //Post.PostCategories = postCategories;
        await postService.CreateNewPost(Post, postCategories, image);

        return Page();
    }
}
