using System.Security.Claims;
using DotNetIdentity.Helpers;
using DotNetIdentity.IdentitySettings;
using DotNetIdentity.Models.Identity;
using DotNetIdentity.Models.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DotNetIdentity.Services.SettingsService;
using Microsoft.AspNetCore.Localization;

namespace DotNetIdentity.Controllers
{
    /// <summary>
    /// controller for culture settings
    /// </summary>
    public class CultureController : Controller
    {
        /// <summary>
        /// controller POST method to set current culture
        /// </summary>
        /// <param name="culture">the culture as string</param>
        /// <param name="returnUrl">the return url as string</param>
        /// <returns>a local redirect to variable returnUrl</returns>
        [HttpPost]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

    }
}