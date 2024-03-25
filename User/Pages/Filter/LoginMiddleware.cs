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
        if (path == "" || path == "/" || URIConstant.WhiteListUris.Any(uri => (path + "/").StartsWith(uri.ToLower())))
        {
            await _next(httpContext);
            return;
        }

        var userString = httpContext.Session.GetString("CREDENTIAL");
        var userConvert = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;

        if (userConvert == null)
        {
            httpContext.Response.Redirect(URIConstant.Login);
            return;
        }

        if (userConvert.RoleId == (int)RoleEnum.Audience && URIConstant.AudienceListUris.Any(uri => (path + "/").StartsWith(uri.ToLower())))
        {
            await _next(httpContext);
            return;
        }
        else if (userConvert.RoleId == (int)RoleEnum.Creator && URIConstant.CreatorListUris.Any(uri => (path + "/").StartsWith(uri.ToLower())))
        {
            await _next(httpContext);
            return;
        }

        httpContext.Response.Redirect(URIConstant.HomePage);
    }
}
