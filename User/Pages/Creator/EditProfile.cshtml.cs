using ArtHubBO.Constants;
using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Models;
using ArtHubService.Interface;
using ArtHubService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net;
using System.Security.Principal;

namespace User.Pages.Creator;

public class EditProfileModel : PageModel
{
    private readonly IAccountService accountService;
    private readonly IConfiguration configuration;
    private readonly IStorageService storageService;

    public int TypeUpdate { get; set; } = 1;
    [BindProperty]
    public AccountUpdateDto AccountUpdate { get; set; }
    [BindProperty]
    public AccountUpdateTypeDto AccountUpdateType { get; set; }
    [BindProperty]
    public IFormFile FileUpload { get; set; }

    public EditProfileModel(IAccountService accountService, IConfiguration configuration, IStorageService storageService)
    {
        this.accountService = accountService;
        this.configuration = configuration;
        this.storageService = storageService;
    }

    public void OnGet(int typeUpdate)
    {
        if (typeUpdate > 0) { 
            TypeUpdate = typeUpdate;
        }
        var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");
        if (accountEmail != null ) {
            var account = accountService.GetAccountIncludeArtistByEmail(accountEmail);
            AccountUpdate = AccountToAccountUpdateDto(account);
            AccountUpdateType = AccountToAccountUpdateTypeDto(account, TypeUpdate);
        }
        return;
    }

    public async Task<IActionResult> OnPostUpdateProfileInfoAsync()
    {
        var gender = Request.Form["Gender"];
        if (gender == Gender.Male.ToString() || gender == Gender.Female.ToString())
        {
            AccountUpdate.Gender = gender.ToString();
        }
        else
        {
            ViewData["ErrorMessageGender"] = "Please choose gender!";
            return Page();
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
                //todo
            } else
            {
                AccountUpdate.Avatar = responseUploadImage.LinkSource;
            }            
        }
        Account? updatedAccount = await accountService.UpdateArtistProfile(AccountUpdate);
        // Configure JsonSerializerSettings to handle reference loops
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var accountJson = JsonConvert.SerializeObject(updatedAccount, settings);
        HttpContext.Session.SetString("CREDENTIAL", accountJson);
        return Page();
    }

        private AccountUpdateDto AccountToAccountUpdateDto(Account account)
    {
        return new AccountUpdateDto
        {
            Email = account.Email,
            FirstName = account.FirstName,
            LastName = account.LastName,
            Gender = account.Gender,
            Avatar = account.Avatar,
            ArtistName = account.Artist!.ArtistName,
            Bio = account.Artist.Bio
        };
    }

    private AccountUpdateTypeDto AccountToAccountUpdateTypeDto(Account account, int typeUpdate)
    {
        AccountUpdateTypeDto accountTypeUpdate = new AccountUpdateTypeDto();
        accountTypeUpdate.TypeUpdate = typeUpdate;
        accountTypeUpdate.Name = account.Artist!.ArtistName;
        accountTypeUpdate.Avatar = account.Avatar;
        if (typeUpdate == 2)
        {
            accountTypeUpdate.NameTypeUpdate = "Change password";
        } else if (typeUpdate == 3)
        {
            accountTypeUpdate.NameTypeUpdate = "Subscribe fee";
        } else if (typeUpdate == 4)
        {
            accountTypeUpdate.NameTypeUpdate = "Delete account";
        } else
        {
            accountTypeUpdate.NameTypeUpdate = "Edit profile";
        }
        return accountTypeUpdate;
    }
}
