using System.Security.Claims;
using DotNetIdentity.Models;
using DotNetIdentity.Data;
using DotNetIdentity.Models.Identity;
using DotNetIdentity.Models.ViewModels;
using DotNetIdentity.Models.DataModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;
using DatatableJS.Data;

namespace DotNetIdentity.Controllers
{
    /// <summary>
    /// Controller Class for Admin views
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Property of type Microsoft.AspNetCore.Identity.UserManager
        /// </summary>
        private readonly UserManager<AppUser> _userManager;
        /// <summary>
        /// Property of type Microsoft.AspNetCore.Identity.RoleManager
        /// </summary>
        private readonly RoleManager<AppRole> _roleManager;
        /// <summary>
        /// Property of type Microsoft.AspNetCore.Identity.SignInManager
        /// </summary>
        private readonly SignInManager<AppUser> _signInManager;
        /// <summary>
        /// Property of type Microsoft.Extensions.Configuration.IConfiguration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Property of type Microsoft.Extensions.Logging.ILogger
        /// </summary>
        private readonly ILogger<AdminController> _logger;
        /// <summary>
        /// Property of type DotNetIdentity.Data.AppDbContext
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Controller Class constructor
        /// </summary>
        /// <param name="db">type DotNetIdentity.Data.AppDbContext</param>
        /// <param name="log">type Microsoft.Extensions.Logging.ILogger</param>
        /// <param name="conf">type Microsoft.Extensions.Configuration.IConfiguration</param>
        /// <param name="userManager">type Microsoft.AspNetCore.Identity.UserManager</param>
        /// <param name="roleManager">type Microsoft.AspNetCore.Identity.RoleManager</param>
        /// <param name="signInManager">type Microsoft.AspNetCore.Identity.SignInManager</param>
        public AdminController(AppDbContext db, ILogger<AdminController> log, IConfiguration conf, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = conf;
            _logger = log;
            _context = db;
        }

        /// <summary>
        /// Controller Action for Index
        /// </summary>
        /// <returns>View of type DotnetIdentity.Views.Admin.Index</returns>
        public IActionResult Index() => View();
        /// <summary>
        /// Controller Action for SystemLogs
        /// </summary>
        /// <returns>View of type DotnetIdentity.Views.Admin.SystemLogs</returns>
        public IActionResult SystemLogs() => View();
        /// <summary>
        /// Controller Action for AutditLogs
        /// </summary>
        /// <returns>View of type DotnetIdentity.Views.Admin.AuditLogs</returns>
        public IActionResult AuditLogs() => View();
        /// <summary>
        /// Controller Action for ErrorLogs
        /// </summary>
        /// <returns>View of type DotnetIdentity.Views.Admin.Errorogs</returns>
        public IActionResult ErrorLogs() => View();
        /// <summary>
        /// Controller Action for Users
        /// </summary>
        /// <returns>View of type DotnetIdentity.Views.Admin.Users</returns>
        public async Task<IActionResult> Users() => View(await _userManager.Users.ToListAsync());      
        /// <summary>
        /// Controller Action for Roles
        /// </summary>
        /// <returns>View of type DotnetIdentity.Views.Admin.Users</returns>  
        public async Task<IActionResult> Roles() => View(await _roleManager.Roles.ToListAsync());

        /// <summary>
        /// Controller Post Method to fetch ErrorLogs
        /// </summary>
        /// <param name="request">type DatatablesJs.Data.DataRequest</param>
        /// <returns>Jason-Array of DatatablesJs.Data.DataResult</returns>
        [HttpPost]
        public IActionResult GetErrorLogs(DataRequest request)
        {
            DataResult<AppLogs> result = _context.AppLogs!.Where(l=>l.Message.ToLower().StartsWith("audit")==false && Convert.ToInt32(l.Level) == 4).ToDataResult(request);
            return Json(result);
        }

        /// <summary>
        /// Controller Post Method to fetch Applicationlogs
        /// </summary>
        /// <param name="request">type DatatablesJs.Data.DataRequest</param>
        /// <returns>Jason-Array of DatatablesJs.Data.DataResult</returns>
        [HttpPost]
        public IActionResult GetAppLogs(DataRequest request)
        {
            DataResult<AppLogs> result = _context.AppLogs!.Where(l=>l.Message.ToLower().StartsWith("audit")==false && Convert.ToInt32(l.Level) < 4).ToDataResult(request);
            return Json(result);
        }

        /// <summary>
        /// Controller Post Method to fetch AuditLogs
        /// </summary>
        /// <param name="request">type DatatablesJs.Data.DataRequest</param>
        /// <returns>Jason-Array of DatatablesJs.Data.DataResult</returns>
        [HttpPost]
        public IActionResult GetAuditLogs(DataRequest request)
        {
            DataResult<AppLogs> result = _context.AppLogs!.Where(l=>l.Message.ToLower().StartsWith("audit")==true).ToDataResult(request);
            return Json(result);
        }

        /// <summary>
        /// Controller Post Method to fetch Users
        /// </summary>
        /// <param name="request">type DatatablesJs.Data.DataRequest</param>
        /// <returns>Jason-Array of DatatablesJs.Data.DataResult</returns>
        [HttpPost]
        public JsonResult GetUsers(DataRequest request) {
            DataResult<AppUser> result = _userManager.Users.ToDataResult(request);
            return Json(result);
        }

        /// <summary>
        /// Controller Action for NewUser view
        /// </summary>
        /// <returns>view of type DotnetIdentity.Views.Admin.NewUser</returns>
        public async Task<IActionResult> NewUser() {
            var mod = new NewUserModel();
            mod.Roles = await _roleManager.Roles.Select(s => new AssignRoleViewModel
            {
                RoleId = s.Id,
                RoleName = s.Name,
                IsAssigned = false
            }).ToListAsync();

            return View(mod);
        }

        /// <summary>
        /// controler Post Method to save a new User
        /// </summary>
        /// <param name="viewModel">type DotnetIdentity.Models.ViewModels.NewUserModel</param>
        /// <returns>view of type DotnetIdentity.Views.Admin.NewUser</returns>
        [HttpPost]
        public async Task<ActionResult> NewUser(NewUserModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser();
                user.IsEnabled = true;
                user.IsMfaForce = viewModel.IsMfaForce;
                user.IsLdapLogin = viewModel.IsLdapLogin;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.Email = viewModel.Email;
                user.EmailConfirmed = true;
                user.Gender = viewModel.Gender;
                user.Department = viewModel.Department.ToString();
                user.UserName = viewModel.UserName;

                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    // roles
                    foreach (var item in viewModel.Roles)
                    {
                        if (item.IsAssigned)
                        {
                            await _userManager.AddToRoleAsync(user, item.RoleName);
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                        }
                    }
                }

                ViewData["message"] = "User created success fully.";
                _logger.LogInformation("AUDIT: " + User.Identity!.Name + " created new user " + user.UserName);
            }
            return View(new NewUserModel());
        }

        /// <summary>
        /// Controller Action for view UpsertRole
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view of type Dotnetidentity.Views.Admin.UpsertRole</returns>
        public async Task<IActionResult> UpsertRole(string? id)
        {
            if (id != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                return View(role.Adapt<UpsertRoleViewModel>());
            }
            
            return View(new UpsertRoleViewModel());
        }

        /// <summary>
        /// Controller GET Method to disable a user account
        /// </summary>
        /// <param name="UserId">the id of the user</param>
        /// <returns>json object (true / false)</returns>
        [HttpGet]
        public async Task<ActionResult> DisableUser(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsEnabled = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " disabled user " + user.UserName);
            return Json(new {success = true});
        }

        /// <summary>
        /// Controller GET Method to enable a user account
        /// </summary>
        /// <param name="UserId">the id of the user</param>
        /// <returns>json object (true / false)</returns>
        [HttpGet]
        public async Task<ActionResult> EnableUser(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsEnabled = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " enabled user " + user.UserName);
            return Json(new {success = true});
        }

        /// <summary>
        /// Controller GET Method to enable ldap login for a user account
        /// </summary>
        /// <param name="UserId">the id of the user</param>
        /// <returns>json object (true / false)</returns>
        [HttpGet]
        public async Task<ActionResult> EnableLdapLogin(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsLdapLogin = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " enabled LDAP login for user " + user.UserName);
            return Json(new {success = true});
        }

        /// <summary>
        /// Controller GET Method to disable ldap login for a user account
        /// </summary>
        /// <param name="UserId">the id of the user</param>
        /// <returns>json object (true / false)</returns>
        [HttpGet]
        public async Task<ActionResult> DisableLdapLogin(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsLdapLogin = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " disabled ldap login for user " + user.UserName);
            return Json(new {success = true});
        }

        /// <summary>
        /// Controller GET Method to enable mfa enforcement for a user account
        /// </summary>
        /// <param name="UserId">the id of the user</param>
        /// <returns>json object (true / false)</returns>
        [HttpGet]
        public async Task<ActionResult> EnableMfaForce(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsMfaForce = true;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " enabled mfa enforce for user " + user.UserName);
            return Json(new {success = true});
        }

        /// <summary>
        /// Controller GET Method to disable mfa enforcement for a user account
        /// </summary>
        /// <param name="UserId">the id of the user</param>
        /// <returns>json object (true / false)</returns>
        [HttpGet]
        public async Task<ActionResult> DisableMfaForce(string UserId) {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user==null) {
                return Json(new { success = false });
            }
            user.IsMfaForce = false;
            await _userManager.UpdateAsync(user);
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " disabled mfa enforce for user " + user.UserName);
            return Json(new {success = true});
        }

        /// <summary>
        /// Controller POST Method to enable a user account
        /// </summary>
        /// <param name="viewModel">tmodel of type Dotnetidentity.Models.ViewModels.UpsertRoleViewModel</param>
        /// <returns>redirect to controller action Roles</returns>
        [HttpPost]
        public async Task<IActionResult> UpsertRole(UpsertRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isUpdate = viewModel.Id != null;

                var role = isUpdate ? await _roleManager.FindByIdAsync(viewModel.Id) : new AppRole() { Name = viewModel.Name, CreatedOn = DateTime.Now };

                if (isUpdate)
                {
                    role.Name = viewModel.Name;
                }

                var result = isUpdate ? await _roleManager.UpdateAsync(role) : await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {                    
                    if(isUpdate) {
                        _logger.LogInformation("AUDIT: " + User.Identity!.Name + " changed name of " + viewModel.Id + " to " + viewModel.Name);
                    } else {
                        _logger.LogInformation("AUDIT: " + User.Identity!.Name + " created role " + viewModel.Name);
                    }
                    return RedirectToAction("Roles");
                }
                result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                result.Errors.ToList().ForEach(f => TempData["Error"] += f.Description);
            }
            
            //return View(viewModel);
            //return View("Roles", await _roleManager.Roles.ToListAsync());
            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Controller POST Method to delete a user account
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>redirect to controller action Users</returns>
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id) {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null) {
                var result = await _userManager.DeleteAsync(user);
                if(!result.Succeeded) {
                    result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
            } else {
                ModelState.AddModelError(string.Empty, "User not found.");
            }
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " deleted user " + id);
            return RedirectToAction("Users");
        }

        /// <summary>
        /// Controller POST Method to enable a role
        /// </summary>
        /// <param name="id">id of the role</param>
        /// <returns>redirect to controller action Roles</returns>
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Role not found.");
            }
            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " deleted role " + id);
            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Controller action to serve the EditUserView
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns>view of type DotNetIdentity.Models.ViewModels.EditUserViewModel</returns>
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Users");
            }
            var viewModel = user.Adapt<EditUserViewModel>();

            var userRoles = await _userManager.GetRolesAsync(user);
            viewModel.Roles = await _roleManager.Roles.Select(s => new AssignRoleViewModel
            {
                RoleId = s.Id,
                RoleName = s.Name,
                IsAssigned = userRoles.Any(a => a == s.Name)
            }).ToListAsync();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var departmentClaim = userClaims.FirstOrDefault(f => f.Type == "Department");
            if (departmentClaim != null)
            {
                viewModel.Department = Enum.Parse<Department>(departmentClaim.Value);
            }
            
            return View(viewModel);
        }

        /// <summary>
        /// Controller POST Method to save user changes
        /// </summary>
        /// <param name="viewModel">a model of type Dotnetidentity.Models.ViewModels.EditUserViewModel</param>
        /// <returns>view of type Dotnetidentity.Views.Admin.EditUser</returns>
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                if (user != null)
                {
                    if (user.PhoneNumber != viewModel.PhoneNumber && _userManager.Users.Any(a => a.PhoneNumber == viewModel.PhoneNumber))
                    {
                        ModelState.AddModelError(string.Empty, "Phone number already in use.");
                    }
                    else
                    {
                        user.FirstName = viewModel.FirstName;
                        user.LastName = viewModel.LastName;
                        user.UserName = viewModel.UserName;
                        user.Email = viewModel.Email;
                        user.PhoneNumber = viewModel.PhoneNumber;
                        user.Gender = viewModel.Gender;
                        user.Department = viewModel.Department.ToString();

                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            // Roles
                            foreach (var item in viewModel.Roles)
                            {
                                if (item.IsAssigned)
                                {
                                    await _userManager.AddToRoleAsync(user, item.RoleName);
                                }
                                else
                                {
                                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                                }
                            }

                            // Claims
                            var userClaims = await _userManager.GetClaimsAsync(user);
                            var departmentClaim = userClaims.FirstOrDefault(a => a.Type == "Department");
                            var claimsToAdd = new Claim("Department", Enum.GetName(viewModel.Department)!);
                            if (departmentClaim != null)
                            {
                                await _userManager.ReplaceClaimAsync(user, departmentClaim, claimsToAdd);
                            }
                            else
                            {
                                await _userManager.AddClaimAsync(user, claimsToAdd);
                            }
                            _logger.LogInformation("AUDIT: " + User.Identity!.Name + " modified user " + user.UserName);
                            return RedirectToAction("Users");
                        }
                        result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            
            return View(viewModel);
        }
    }
}