using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIdentity.Controllers
{
    public class ClaimBasedController : Controller
    {
        [Authorize(Policy = "HrDepartmentPolicy")]
        public IActionResult HR() => View();

        [Authorize(Policy = "SoftwareDepartmentPolicy")]
        public IActionResult Software() => View();

        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult Employee() => View();

        [Authorize(Policy = "FreeTrialPolicy")]
        public IActionResult FreeTrial() => View();

        [Authorize(Policy = "AtLeast18Policy")]
        public IActionResult Violence() => View();
    }
}