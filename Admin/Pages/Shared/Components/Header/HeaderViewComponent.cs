using ArtHubBO.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.Pages.Shared.Components.Header;

public class HeaderViewComponent : ViewComponent
{
    private Account user;
    
    public IViewComponentResult Invoke()
    {
        var httpContext = ViewContext.HttpContext;
        var userString = httpContext.Session.GetString("CREDENTIAL");
        user = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;
        
        return View(user);
    }
}