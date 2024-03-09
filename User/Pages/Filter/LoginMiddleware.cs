using ArtHubBO.Entities;
using ArtHubBO.Enum;
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
        var userString = httpContext.Session.GetString("CREDENTIAL");
        if (!URIConstant.WhiteListUris.Any(uri => uri.ToLower().Equals(path)))
        {
            if (userString != null)
            {
                var userConvert = JsonConvert.DeserializeObject<Account>(userString);
                if (userConvert != null)
                {
                    if (userConvert.Role.RoleName.Equals(RoleEnum.Audience.ToString()) && URIConstant.AudienceListUris.Any(uri => uri.ToLower().Equals(path)))
                    {
                        await _next(httpContext);
                        return;
                    }
                    else if (userConvert.Role.RoleName.Equals(RoleEnum.Creator.ToString()) && URIConstant.CreatorListUris.Any(uri => uri.ToLower().Equals(path)))
                    {
                        await _next(httpContext);
                        return;
                    }
                    else
                    {
                        httpContext.Response.Redirect(URIConstant.Login);
                        return;
                    }
                }
            }            
            else
            {
                httpContext.Response.Redirect(URIConstant.Login);
                return;
            }
        }
        else if (URIConstant.Login.Equals(path) && userString != null)
        {
            httpContext.Response.Redirect(URIConstant.HomePage);
            return;
        }
        await _next(httpContext);
    }
}
