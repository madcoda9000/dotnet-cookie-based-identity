using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIdentity.Controllers
{
    public class RoleBasedController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard() => View();

        [Authorize(Roles = "Moderator, Admin")]
        public IActionResult Users() => View();

        [Authorize(Roles = "Editor, Moderator, Admin")]
        public IActionResult Articles() => View();

        [Authorize(Roles = "User, Editor, Moderator, Admin")]
        public IActionResult Profile() => View();
    }
}