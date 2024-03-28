using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubService.Interface;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using User.Pages.Filter;

namespace User.Pages.Authenticate;

public class RegisterModel : PageModel
{
    private readonly IAccountService accountService;

    public RegisterModel(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [BindProperty]
    public AccountRegisterDto AccountRegisterDto { get; set; } = new AccountRegisterDto();

    public async Task<IActionResult> OnPost()
    {
        if (!validateInput(AccountRegisterDto))
        {
            return Page();

        } else
        {
            if (AccountRegisterDto.IsRegisterCreator)
            {
                var isCreated = await accountService.CreateAccount(AccountRegisterDtoToAccount(AccountRegisterDto));
                if (isCreated)
                {
                    TempData["SuccessMessageRegister"] = "Your registration was successful! Please proceed to login.";
                    return RedirectToPage(URIConstant.Login);
                } else
                {
                    TempData["FailMessageRegister"] = "Duplicate email. Registration fail!";
                    return Page();
                }
                
            } else
            {
                var isCreated = await accountService.CreateAccount(AccountRegisterDtoToAccount(AccountRegisterDto));
                if (isCreated)
                {
                    TempData["SuccessMessageRegister"] = "Your registration was successful! Please proceed to login.";
                    return RedirectToPage(URIConstant.Login);
                } else
                {
                    TempData["FailMessageRegister"] = "Duplicate email. Registration fail!";
                    return Page();
                }                
            }
        }            
    }


    private Account AccountRegisterDtoToAccount(AccountRegisterDto accountRegister)
    {
        var gender = Request.Form["Gender"];
        Account account = new Account();        
        account.Email = accountRegister.Email;
        account.Password = Encryption.Encrypt(accountRegister.Password);
        account.FirstName = accountRegister.FirstName;
        account.LastName = accountRegister.LastName;
        account.Gender = gender!;
        account.Status = (int)AccountStatus.Normal;
        account.Enabled = true;
        account.Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTii3hM5abJEj_Zu0wumINDLGvHaT-MZaesc9dj-gXSUg&s";
        
        if (accountRegister.IsRegisterCreator)
        {
            account.Artist = new Artist();
            account.Artist.Email = accountRegister.Email;
            account.Artist.ArtistName = accountRegister.ArtistName;
            account.Artist.Bio = string.Empty;
            account.Artist.Fees = new List<Fee> {
                new Fee {
                    Amount = 0,
                    ArtistEmail = accountRegister.Email
                }
            };
            account.RoleId = (int)RoleEnum.Creator + 1;
        } else
        {
            account.RoleId = (int)RoleEnum.Audience + 1;
        }
        return account;
    }

    private bool validateInput(AccountRegisterDto accountRegisterDto)
    {
        var splitFirstNameList = accountRegisterDto.FirstName.Trim().Split(" ");
        foreach (var split in splitFirstNameList)
        {
            if (!char.IsUpper(split.Trim()[0]))
            {
                ViewData["ErrorMessageFirstNameFirstLetter"] = "Please input first name each word with capital letter";
                return false;
            }
        }
        var splitLastNameList = accountRegisterDto.LastName.Trim().Split(" ");
        foreach (var split in splitLastNameList)
        {
            if (!char.IsUpper(split.Trim()[0]))
            {
                ViewData["ErrorMessageLastNameFirstLetter"] = "Please input last name each word with capital letter";
                return false;
            }
        }
        if (accountRegisterDto.IsRegisterCreator)
        {
            var splitArtistNameList = accountRegisterDto.ArtistName.Trim().Split(" ");
            foreach (var split in splitArtistNameList)
            {
                if (!char.IsUpper(split.Trim()[0]))
                {
                    ViewData["ErrorMessageArtistNameFirstLetter"] = "Please input artist name each word with capital letter";
                    return false;
                }
            }
        }
        return true;
    }
}
