using ArtHubBO.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Utils
{
    public static class SessionUtil
    {
        public static Account GetAuthenticatedAccount(HttpContext httpContext)
        {
            var userString = httpContext?.Session?.GetString("CREDENTIAL");
            var user = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;
            return user;
        }
    }
}
