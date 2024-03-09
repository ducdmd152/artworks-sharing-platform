using ArtHubBO.Constants;
using ArtHubBO.Models;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator;

public class UploadNewArtworkModel : PageModel
{
    private readonly IConfiguration configuration;
    private readonly IStorageService storageService;
    [BindProperty]
    public IFormFile FileUpload { get; set; }

    public UploadNewArtworkModel(IConfiguration configuration, IStorageService storageService)
    {
        this.configuration = configuration;
        this.storageService = storageService;
    }

    public async Task<IActionResult> OnPost()
    {
        await using var memoryStream = new MemoryStream();
        await FileUpload.CopyToAsync(memoryStream);
        var fileExt = Path.GetExtension(FileUpload.Name);
        var objName = $"{Guid.NewGuid()}.{fileExt}";
        var s3Obj = new S3ObjectModel()
        {
            BucketName = configuration[Constants.BucketName] ?? "",
            InputStream = memoryStream,
            Name = objName
        };

        var credential = new AwsCredentials()
        {
            AwsKey = configuration[Constants.AccessKey] ?? "",
            AwsSecret = configuration[Constants.SecretKey] ?? ""
        };
        var result = await storageService.UploadFileAsync(s3Obj, credential);
        return Page();
    }
}
