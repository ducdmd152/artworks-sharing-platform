using ArtHubBO.Constants;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Models;
using ArtHubBO.DTO;
using ArtHubService.Interface;
using ArtHubService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using User.Pages.Filter;
using System.Net;

namespace User.Pages.Creator;

public class EditPostModel : PageModel
{
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;
    private readonly IConfiguration configuration;
    private readonly IStorageService storageService;

    [BindProperty]
    public PostUpdateDto Post { get; set; }
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

    public EditPostModel(IPostService postService, ICategoryService categoryService, IConfiguration configuration, IStorageService storageService)
    {
        this.postService = postService;
        this.categoryService = categoryService;
        this.configuration = configuration;
        this.storageService = storageService;
    }

    public void OnGet(int postId)
    {
        Post = ConvertPostToPostUpdateDto(postService.Get(postId));
        Categories = categoryService.GetCategories().ToList();
    }

    public async Task<IActionResult> OnPost()
    {
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
        if (selectedCategoryIds.Count() <= 0)
        {
            ViewData["ErrorMessageCategories"] = "Please choose at least one category!";
            return Page();
        }

        var postScope = Request.Form["PostScope"];
        if (Enum.TryParse(postScope, out PostScope parsedScope))
        {
            Post.Scope = (int)parsedScope;
        } else
        {
            ViewData["ErrorMessagePostScope"] = "Please choose post scope!";
            return Page();
        }

        var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");
        if (accountEmail != null)
        {
            Post.ArtistEmail = accountEmail;
        }
        if (FileUpload != null)
        {
            await using var memoryStream = new MemoryStream();
            await FileUpload.CopyToAsync(memoryStream);
            var fileExt = Path.GetExtension(FileUpload.FileName);
            var objName = $"{S3Constants.FolderS3}{Guid.NewGuid()}{fileExt}";
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
            if (responseUploadImage.StatusCode != (int)HttpStatusCode.OK)
            {
                TempData["FailUploadImage"] = "Upload image fail!";
                return Page();
            }
            Post.Images.First().ImageUrl = responseUploadImage.LinkSource;
        }
        
                 

        // Post category

        if (Post.PostCategories != null)
        {
            selectedCategoryIds.ToList().ForEach(select =>
            {
                if (!Post.PostCategories.Any(pc => pc.CategoryId == select))
                {
                    Post.PostCategories.Add(new PostCategory
                    {
                        CategoryId = select
                    });
                }
            });

            Post.PostCategories.ToList().ForEach(select =>
            {
                if (!selectedCategoryIds.ToList().Contains(select.CategoryId))
                {
                    Post.PostCategories.Remove(select);
                }
            });
        } else
        {
            Post.PostCategories = new List<PostCategory>();
            selectedCategoryIds.ToList().ForEach(select =>
            {                
                Post.PostCategories.Add(new PostCategory
                {
                    CategoryId = select
                });
            });
        }
        

        var updatedPost = await postService.UpdatePost(Post).ConfigureAwait(false);
        if (updatedPost == null)
        {
            TempData["FailUpdatePost"] = "Update post fail!";
            return Page();
        }
        TempData["UpdatePostSuccess"] = "Edit post "+ updatedPost.Title +" success!";
        return RedirectToPage(URIConstant.ArtworkList);
    }

    private PostUpdateDto ConvertPostToPostUpdateDto(Post post)
    {
        return new PostUpdateDto
        {
            PostId = post.PostId,
            ArtistEmail = post.ArtistEmail,
            Description = post.Description,
            Scope = post.Scope,
            Title = post.Title,
            Images = post.Images,
            PostCategories = post.PostCategories
        };
    }
}
