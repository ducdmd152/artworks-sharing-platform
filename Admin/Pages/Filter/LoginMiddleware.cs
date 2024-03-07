using ArtHubBO.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace User.Pages.Filter;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var path = httpContext.Request.Path.ToString().ToLower();
        
        if (!URIConstant.WhiteListUris.Any(uri => uri.ToLower().Equals(path)))
        {
            var userString = httpContext.Session.GetString("CREDENTIAL");
            if (userString != null)
            {
                var userConvert = JsonConvert.DeserializeObject<Account>(userString);
                if (userConvert != null)
                {
                    if (userConvert.Role.RoleName.Equals("moderator") && URIConstant.ModeratorListUris.Any(uri => uri.ToLower().Equals(path)))
                    {
                        await _next(httpContext);
                        return;
                    }
                    else if (userConvert.Role.RoleName.Equals("admin") && URIConstant.AdminListUris.Any(uri => uri.ToLower().Equals(path)))
                    {
                        await _next(httpContext);
                        return;
                    }
                    else
                    {
                        httpContext.Response.Redirect("/Login");
                        return;
                    }
                }
            }
            else
            {
                httpContext.Response.Redirect("/Login");
                return;
            }
        }
        await _next(httpContext);
    }
}
