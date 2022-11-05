using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIdentity.Controllers
{
    /// <summary>
    /// Controller to test ClaimBased Authorization
    /// </summary>
    public class ClaimBasedController : Controller
    {
        /// <summary>
        /// Controller action for HR view
        /// </summary>
        /// <returns>view HR</returns>
        [Authorize(Policy = "HrDepartmentPolicy")]
        public IActionResult HR() => View();

        /// <summary>
        /// Controller action for Software view
        /// </summary>
        /// <returns>view Software</returns>
        [Authorize(Policy = "SoftwareDepartmentPolicy")]
        public IActionResult Software() => View();

        /// <summary>
        /// Controller action for Employee view
        /// </summary>
        /// <returns>view Employee</returns>
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult Employee() => View();

        /// <summary>
        /// Controller action for FreeTrial view
        /// </summary>
        /// <returns>view FreeTrial</returns>
        [Authorize(Policy = "FreeTrialPolicy")]
        public IActionResult FreeTrial() => View();

        /// <summary>
        /// Controller action for AtLeast18Policy view
        /// </summary>
        /// <returns>view AtLeast18Policy</returns>
        [Authorize(Policy = "AtLeast18Policy")]
        public IActionResult Violence() => View();
    }
}