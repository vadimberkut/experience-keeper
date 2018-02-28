using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExperienceKeeper.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool IsAuthenticated(this HttpContext context)
        {
            return context.User.Identity != null && context.User.Identity.IsAuthenticated;
        }
    }
}
