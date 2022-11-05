using DotNetIdentity.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotNetIdentity.Controllers {
/// <summary>
/// Controller for Home views
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Property of type UserManager
    /// </summary>
    private readonly UserManager<AppUser> _userManager;

    /// <summary>
    /// Controller constructor
    /// </summary>
    /// <param name="userManager">Property UserManager</param>
    public HomeController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Controller action for view Index
    /// </summary>
    /// <returns>view Index</returns>
    [Authorize]
    public IActionResult Index() => View(); 

    /// <summary>
    /// controller action for view AccessDenied
    /// </summary>
    /// <returns>view AccessDenied</returns>
    public IActionResult AccessDenied() => View();
}
}