using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIdentity.Controllers
{
    /// <summary>
    /// Controller to test RoleBased Authorization
    /// </summary>
    public class RoleBasedController : Controller
    {
        /// <summary>
        /// Controller Action for Dashboard view
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard() => View();

        /// <summary>
        /// Controller Action for Users view
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Moderator, Admin")]
        public IActionResult Users() => View();

        /// <summary>
        /// Controller Action for Articles view
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Editor, Moderator, Admin")]
        public IActionResult Articles() => View();

        /// <summary>
        /// Controller Action for Profile view
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User, Editor, Moderator, Admin")]
        public IActionResult Profile() => View();
    }
}